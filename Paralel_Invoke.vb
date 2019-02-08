Module ParallelTasks
    Sub Parallel_Invoke()
        ' Retrieve Goncharov's "Oblomov" from Gutenberg.org.
        Dim words As String() = CreateWordArray("http://www.gutenberg.org/files/54700/54700-0.txt")

        Form1.PonTexto("Total palabras encontradas = " & words.Count)

        '#Region "ParallelTasks"
        ' Perform three tasks in parallel on the source array
        Dim opt As New ParallelOptions
        opt.MaxDegreeOfParallelism = 1      ' 1 solo procesador (Asi funciona bien)
        '' Si usa más de 1 procesador, no rellena bien el TextBox.
        Parallel.Invoke(opt, Sub()
                                 Form1.PonTexto("Begin first task...")
                                 GetLongestWord(words)
                                 ' close first Action
                             End Sub,
                        Sub()
                            Form1.PonTexto("Begin second task...")
                            GetMostCommonWords(words)
                            'close second Action
                        End Sub,
                        Sub()
                            Form1.PonTexto("Begin third task...")
                            GetCountForWord(words, "sleep")
                            'close third Action
                        End Sub)
        'close parallel.invoke
        Form1.PonTexto("Returned from Parallel.Invoke")
        '#End Region

        ' Console.WriteLine("Press any key to exit")
        'Console.ReadKey()
    End Sub

#Region "HelperMethods"
    Sub GetCountForWord(ByVal words As String(), ByVal term As String)
        Dim findWord = From word In words
                       Where word.ToUpper().Contains(term.ToUpper())
                       Select word

        Form1.PonTexto("Task 3 -- The word " & term & " occurs " & findWord.Count() & " times.")
    End Sub

    Sub GetMostCommonWords(ByVal words As String())
        Dim frequencyOrder = From word In words
                             Where word.Length > 6
                             Group By word
                             Into wordGroup = Group, Count()
                             Order By wordGroup.Count() Descending
                             Select wordGroup

        Dim commonWords = From grp In frequencyOrder
                          Select grp
                          Take (10)

        Form1.PonTexto("Task 2 -- The most common words are:")
        For Each v In commonWords
            Form1.PonTexto(v(0))
        Next
    End Sub

    Sub GetLongestWord(ByVal words As String())
        Dim longestWord = (From w In words
                           Order By w.Length Descending
                           Select w).First()

        Form1.PonTexto("Task 1 -- The longest word is " & longestWord & ".")
    End Sub


    ' An http request performed synchronously for simplicity.
    Function CreateWordArray(ByVal uri As String) As String()
        Form1.PonTexto("Retrieving from " & uri)

        ' Download a web page the easy way.
        Dim s As String = New System.Net.WebClient().DownloadString(uri)

        ' Separate string into an array of words, removing some common punctuation.
        Return s.Split(New Char() {" "c, ControlChars.Lf, ","c, "."c, ";"c, ":"c,
        "-"c, "_"c, "/"c}, StringSplitOptions.RemoveEmptyEntries)
    End Function
#End Region
End Module
' The exmaple displays output like the following:
'       Retrieving from http://www.gutenberg.org/files/54700/54700-0.txt
'       Begin first task...
'       Begin second task...
'       Begin third task...
'       Task 2 -- The most common words are:
'       Oblomov
'       himself
'       Schtoltz
'       Gutenberg
'       Project
'       another
'       thought
'       Oblomov's
'       nothing
'       replied
'
'       Task 1 -- The longest word is incomprehensible.
'       Task 3 -- The word "sleep" occurs 57 times.
'       Returned from Parallel.Invoke
'       Press any key to exit
