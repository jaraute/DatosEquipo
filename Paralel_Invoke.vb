Module ParallelTasks
    Sub Parallel_Task()
        ' Retrieve Goncharov's "Oblomov" from Gutenberg.org.
        Dim words As String() = CreateWordArray("http://www.gutenberg.org/files/54700/54700-0.txt")

        frmInicio.PonTexto("Total palabras encontradas = " & words.Count)

        '#Region "ParallelTasks"
        ' Perform three tasks in parallel on the source array
        Dim opt As New ParallelOptions
        opt.MaxDegreeOfParallelism = 1      ' 1 solo procesador (Asi funciona bien)
        '' Si usa más de 1 procesador, no rellena bien el TextBox.
        Dim t1 As Task = New Task(New Action(Sub()
                                                 frmInicio.PonTexto("Begin first task...")
                                                 GetLongestWord(words)
                                             End Sub))
        t1.RunSynchronously()
        t1.Wait()
        Dim t2 As Task = New Task(New Action(Sub()
                                                 frmInicio.PonTexto("Begin second task...")
                                                 GetMostCommonWords(words)
                                             End Sub))
        t2.RunSynchronously()
        t2.Wait()
        Dim t3 As Task = New Task(New Action(Sub()
                                                 frmInicio.PonTexto("Begin third task...")
                                                 GetCountForWord(words, "sleep")
                                             End Sub))
        t3.RunSynchronously()
        t3.Wait()
        frmInicio.PonTexto("Returned from Task")

    End Sub
    Sub Parallel_Invoke()
        ' Retrieve Goncharov's "Oblomov" from Gutenberg.org.
        Dim words As String() = CreateWordArray("http://www.gutenberg.org/files/54700/54700-0.txt")

        frmInicio.PonTexto("Total palabras encontradas = " & words.Count)

        '#Region "ParallelTasks"
        ' Perform three tasks in parallel on the source array
        Dim opt As New ParallelOptions
        opt.MaxDegreeOfParallelism = 1      ' 1 solo procesador (Asi funciona bien)
        '' Si usa más de 1 procesador, no rellena bien el TextBox.
        Parallel.Invoke(opt, Sub()
                                 frmInicio.PonTexto("Begin first task...")
                                 GetLongestWord(words)
                                 ' close first Action
                             End Sub,
                        Sub()
                            frmInicio.PonTexto("Begin second task...")
                            GetMostCommonWords(words)
                            'close second Action
                        End Sub,
                        Sub()
                            frmInicio.PonTexto("Begin third task...")
                            GetCountForWord(words, "sleep")
                            'close third Action
                        End Sub)
        frmInicio.PonTexto("Returned from Parallel.Invoke")
    End Sub

#Region "HelperMethods"
    Sub GetCountForWord(ByVal words As String(), ByVal term As String)
        Dim findWord = From word In words
                       Where word.ToUpper().Contains(term.ToUpper())
                       Select word

        frmInicio.PonTexto("Task 3 -- The word " & term & " occurs " & findWord.Count() & " times.")
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

        frmInicio.PonTexto("Task 2 -- The most common words are:")
        For Each v In commonWords
            frmInicio.PonTexto(v(0))
        Next
    End Sub

    Sub GetLongestWord(ByVal words As String())
        Dim longestWord = (From w In words
                           Order By w.Length Descending
                           Select w).First()

        frmInicio.PonTexto("Task 1 -- The longest word is " & longestWord & ".")
    End Sub


    ' An http request performed synchronously for simplicity.
    Function CreateWordArray(ByVal uri As String) As String()
        frmInicio.PonTexto("Retrieving from " & uri)

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
