Imports System.Net
Imports System.Net.Sockets
Imports System.Security.Principal
Imports System.Text
Imports System.Text.RegularExpressions
Imports IWshRuntimeLibrary

Public Class Form1
    Public cFtp As clsFTP = Nothing
    Public ficheroLocal As String = "C:\Temp\ErrorFatal_0x0028.png"
    '
    ' ***** Servidor Primario FTP
    Public FTP1_host As String = "ftp://ttu.ulmaconstruction.com"
    Public FTP1_port As String = "21"
    Public FTP1_dir As String = "ftp://ttu.ulmaconstruction.com/logs/Internal/"
    Public FTP1_dirRel As String = "/logs/Internal/"
    Public FTP1_ficheroFtp As String = "ftp://ttu.ulmaconstruction.com/logs/Internal/ErrorFatal_0x0028.png"
    Public FTP1_user As String = "ftp_revit_user"
    Public FTP1_pass As String = "RehU768P"
    ' ***** Servidor Secundario FTP
    Public FTP2_host As String = "ftp://ttu.construccion.ulma.es"
    Public FTP2_port As String = "21"
    Public FTP2_dir As String = "ftp://ttu.construccion.ulma.es/logs/Internal/"
    Public FTP2_dirRel As String = "/logs/Internal/"
    Public FTP2_ficheroFtp As String = "ftp://ttu.construccion.ulma.es/logs/Internal/ErrorFatal_0x0028.png"
    Public FTP2_user As String = "ESOTAWS2\ftp_revit_user"
    Public FTP2_pass As String = "RehU768"
    '
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        cFtp = New clsFTP(FTP1_host, FTP1_dir, FTP1_user, FTP1_pass)
    End Sub
    '' Delegado para escribir en txtDatos.Text
    Public Delegate Sub PonTextoCallBack(text As String)
    '' Metodo privado que llama el delegado
    'Este método demuestra un patrón para realizar llamadas seguras a hilos
    'en un control Windows Forms.
    'Si el hilo que hace la llamada es diferente que el hilo que creo el control
    'ListBox, entonces el método crea un AddItemCallBack y lo manda llamar desde
    'el mismo de manera asíncrona utilizando en método invoke.
    'Si el hilo es el mismo que el que creo el control ListBox, entonces el Ítem
    ' es agregado directamente
    Public Shared Sub PonTexto(ByVal text As String)
        If text.EndsWith(vbCrLf) = False Then text = text & vbCrLf
        If Form1.txtDatos.InvokeRequired Then
            Dim d As New PonTextoCallBack(AddressOf PonTexto)
            Form1.Invoke(d, New Object() {Form1.txtDatos.Text & text})
        Else
            Form1.txtDatos.Text &= text
        End If
    End Sub

    'End Sub
    Public Shared Sub Pon(queTexto As String)
        'If borrar Then
        '    Form1.txtDatos.Text = queTexto & IIf(queTexto.EndsWith(vbCrLf), "", vbCrLf)
        'Else
        Form1.txtDatos.Text &= queTexto & IIf(queTexto.EndsWith(vbCrLf), "", vbCrLf)
        'End If
        Form1.txtDatos.Refresh()
    End Sub

    Function FTP_SubirFichero() As String
        Dim resultado As String = ""
        '
        Try
            My.Computer.Network.UploadFile(
                sourceFileName:=ficheroLocal,
                address:=FTP1_ficheroFtp,
                userName:=FTP1_user,
                password:=FTP1_pass)
        Catch ex As Exception
            resultado = ex.ToString
        End Try

        'Dim miUri As String = "ftp://ftp.midominio.com/carpeta/fichero.jpg"
        'Dim miRequest As Net.FtpWebRequest = Net.WebRequest.Create(miUri)
        'miRequest.Credentials = New Net.NetworkCredential("user", "pass")
        'miRequest.Method = Net.WebRequestMethods.Ftp.UploadFile
        'Try
        '    Dim bFile() As Byte = System.IO.File.ReadAllBytes("C:\carpeta\fichero.jpg")
        '    Dim miStream As System.IO.Stream = miRequest.GetRequestStream()
        '    miStream.Write(bFile, 0, bFile.Length)
        '    miStream.Close()
        '    miStream.Dispose()
        'Catch ex As Exception
        '    Throw New Exception(ex.Message & ". El Archivo no pudo ser enviado.")
        'End Try
        Return resultado
    End Function

    Public Function FTP_Upload() As String
        Dim resultado As String = ""
        Dim fichero As String = "C:\Temp\ErrorFatal_0x0028.png"
        Dim host As String = "ftp://ttu.ulmaconstruction.com/"
        Dim dir As String = "ftp://ttu.ulmaconstruction.com/logs/Internal/"
        Dim destino As String = "ftp://ttu.ulmaconstruction.com/logs/Internal/btbc.info"
        Dim usuario As String = "ftp_revit_user"
        Dim clave As String = "RehU768P"
        '
        Dim cFtp As New clsFTP(host:=host, dir:=dir, user:=usuario, pass:=clave)
        resultado = cFtp.FTP_Upload(fichero, destino)

        Return resultado
    End Function
    Public Function FTP_Sube1() As String
        Dim resultado As String = ""
        Dim fichero As String = "C:\Temp\ErrorFatal_0x0028.png"
        Dim host As String = "ftp://ttu.ulmaconstruction.com/"
        Dim dir As String = "ftp://ttu.ulmaconstruction.com/logs/Internal/"
        Dim destino As String = "ftp://ttu.ulmaconstruction.com/logs/Internal/ErrorFatal_0x0028.png"
        Dim usuario As String = "ftp_revit_user"
        Dim clave As String = "RehU768P"
        '
        'Upload File to FTP site

        'Create Request To Upload File'
        Dim wrUpload As FtpWebRequest = DirectCast(WebRequest.Create _
           (destino), FtpWebRequest)
        'Specify Username & Password'
        wrUpload.Credentials = New NetworkCredential(usuario, clave)
        'Start Upload Process'
        wrUpload.Method = WebRequestMethods.Ftp.UploadFile

        'Locate File And Store It In Byte Array'
        Dim btfile() As Byte = IO.File.ReadAllBytes(fichero)
        Dim strFile As IO.Stream = Nothing
        Try
            'Get File'
            strFile = wrUpload.GetRequestStream()
            'Upload Each Byte'
            strFile.Write(btfile, 0, btfile.Length)
            resultado = destino & " --> Subido"
        Catch ex As Exception
            resultado = ex.ToString
        Finally
            If strFile IsNot Nothing Then
                'Close'
                strFile.Close()
            End If
        End Try
        'Free Memory'
        strFile.Dispose()

        Return resultado
    End Function
    '
    'Download A File From FTP Site'
    Private Function FTP_Baja1(fiFtp As String, fiLocal As String, user As String, pass As String) As String
        Dim resultado As String = ""
        Dim usuario As String = "ftp_revit_user"
        Dim clave As String = "RehU768P"
        '
        'Create Request To Download File'
        'Dim wrDownload As FtpWebRequest = WebRequest.Create("ftp://ftp.test.com/file.txt")
        Dim wrDownload As FtpWebRequest = WebRequest.Create(fiFtp)

        'Specify That You Want To Download A File'
        wrDownload.Method = WebRequestMethods.Ftp.DownloadFile

        'Specify Username & Password'
        wrDownload.Credentials = New NetworkCredential(user, pass)

        'Response Object'
        Dim rDownloadResponse As FtpWebResponse = wrDownload.GetResponse()

        'Incoming File Stream'
        Dim strFileStream As IO.Stream = rDownloadResponse.GetResponseStream()

        'Read File Stream Data'
        Dim srFile As IO.StreamReader = New IO.StreamReader(strFileStream)

        'Console.WriteLine(srFile.ReadToEnd())
        IO.File.WriteAllText(fiLocal, srFile.ReadToEnd)

        'Show Status Of Download'
        resultado = "Download Complete, status " & rDownloadResponse.StatusDescription

        srFile.Close() 'Close

        rDownloadResponse.Close()
        Return resultado
    End Function

    'Delete File On FTP Server'
    Private Function FTP_Borra1(fiFtp As String) As String
        Dim resultado As String = ""
        Dim user As String = "ftp_revit_user"
        Dim pass As String = "RehU768P"

        'Create Request To Delete File'
        Dim wrDelete As FtpWebRequest =
             DirectCast(WebRequest.Create(fiFtp),
             FtpWebRequest)

        wrDelete.Credentials = New NetworkCredential(user, pass)

        'Specify That You Want To Delete A File'
        wrDelete.Method = WebRequestMethods.Ftp.DeleteFile
        'Response Object'
        Dim rDeleteResponse As FtpWebResponse =
             CType(wrDelete.GetResponse(),
             FtpWebResponse)
        'Show Status Of Delete'
        'Console.WriteLine("Delete status: {0}", rDeleteResponse.StatusDescription)
        resultado = "Delete status: " & rDeleteResponse.StatusDescription
        'Close'
        rDeleteResponse.Close()
        Return resultado
    End Function

    Private Sub btnFtpUp_Click(sender As Object, e As EventArgs) Handles btnFtpUp.Click
        Dim dirLogsLocal As String = "C:\Users\alberto.ADA\AppData\Roaming\Autodesk\Revit\Addins\2018\UCRevit2018\logs"
        Dim dirLogsFTP As String = "ftp://ttu.ulmaconstruction.com/logs/Internal"
        ' Recorrer el directorio local y subir todos los ficheros
        For Each fiCsv As String In IO.Directory.GetFiles(dirLogsLocal, "*.csv", IO.SearchOption.TopDirectoryOnly)
            Dim nombre As String = IO.Path.GetFileName(fiCsv)
            Dim fiFtp As String = dirLogsFTP & "/" & nombre
            Dim resultadoTxt As String = cFtp.FTP_Upload(fiCsv, fiFtp).ToString
            If resultadoTxt.StartsWith("CORRECTO") Then
                IO.File.Delete(fiCsv)
            End If
        Next
    End Sub

    Private Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        Dim cFtp As New clsFTP(host:=FTP1_host, dir:=FTP1_dir, user:=FTP1_user, pass:=FTP1_pass)
        Dim lista As String() = cFtp.FTP_ListaDir(FTP1_dir)
        If lista.Length = 1 AndAlso lista(0).ToString.ToUpper = "ERROR" Then
            txtDatos.Text = "Falla la comunicación con el servidor"
        ElseIf lista.Length = 0 Then
            txtDatos.Text = "No hay fichero en el directorio"
        Else
            txtDatos.Text = FTP1_dir & " :" & vbCrLf & vbCrLf & Join(lista, vbCrLf)
        End If
    End Sub

    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        txtDatos.Text = ""
        Dim cFtp As New clsFTP(host:=FTP1_host, dir:=FTP1_dir, user:=FTP1_user, pass:=FTP1_pass)
        Dim lista As String() = cFtp.FTP_ListaDir(FTP1_dir)
        If lista.Length = 1 AndAlso lista(0).ToString.ToUpper = "ERROR" Then
            txtDatos.Text = "Falla la comunicación con el servidor"
        ElseIf lista.Length = 0 Then
            txtDatos.Text = "No hay fichero en el directorio"
        ElseIf lista.Length > 0 Then
            For Each fiCsv As String In lista
                Dim fiFtp As String = FTP1_dir & "/" & fiCsv
                '
                If fiCsv.Contains(Environment.MachineName) = True Then
                    txtDatos.Text &= vbCrLf & cFtp.FTP_Borra(fiFtp)
                Else
                    txtDatos.Text &= vbCrLf & fiFtp & " no borrado."
                End If
            Next
        End If


        'Dim fiFtp As String = "ftp://ttu.ulmaconstruction.com/logs/Internal/ErrorFatal_0x0028.png"
        'txtDatos.Text &= vbCrLf & cFtp.FTP_Borra(fiFtp)
        'fiFtp = "ftp://ttu.ulmaconstruction.com/logs/Internal/fi1.csv"
        'txtDatos.Text &= vbCrLf & cFtp.FTP_Borra(fiFtp)
        'fiFtp = "ftp://ttu.ulmaconstruction.com/logs/Internal/fi1 - copia.csv"
        'txtDatos.Text &= vbCrLf & cFtp.FTP_Borra(fiFtp)
        'fiFtp = "ftp://ttu.ulmaconstruction.com/logs/Internal/fi1 - copia - copia.csv"
        'txtDatos.Text &= vbCrLf & cFtp.FTP_Borra(fiFtp)
        'fiFtp = "ftp://ttu.ulmaconstruction.com/logs/Internal/fi1 - copia - copia - copia.csv"
        'txtDatos.Text &= vbCrLf & cFtp.FTP_Borra(fiFtp)
    End Sub


    Private Sub btnExisteDir_Click(sender As Object, e As EventArgs) Handles btnExisteDir.Click
        txtDatos.Text = cFtp.FTP_ExisteDir(FTP1_dir)
    End Sub

    Private Sub btnImagen_Click(sender As Object, e As EventArgs) Handles btnImagen.Click
        Dim rutaWebImagen As String = "https://scontent-mad1-1.xx.fbcdn.net/v/t1.0-9/49158364_1956079217840920_2849087631206121472_o.jpg?_nc_cat=102&_nc_ht=scontent-mad1-1.xx&oh=75a603d7227e7dbb76e2d1bddb3addf4&oe=5CB71500"
        'Dim rutaWebImagen As String = "https://i.pinimg.com/originals/c9/b8/6b/c9b86b82df865882288a0873c4d7630e.jpg"
        rutaWebImagen = InputBox("Ruta de la imagen", "Descargar imagen", rutaWebImagen)

        If rutaWebImagen.ToLower.StartsWith("http") Then
            Me.pb1.Image = DownloadImage(rutaWebImagen)
        End If
    End Sub

    Function DownloadImage(_URL As String) As Image

        Dim _tmpImage As Image = Nothing

        Try

            Dim _HttpWebRequest As System.Net.HttpWebRequest = CType(System.Net.HttpWebRequest.Create(_URL), System.Net.HttpWebRequest)
            _HttpWebRequest.AllowWriteStreamBuffering = True

            _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)"
            _HttpWebRequest.Referer = "http://www.google.com/"

            _HttpWebRequest.Timeout = 20000


            Dim _WebResponse As System.Net.WebResponse = _HttpWebRequest.GetResponse()
            Dim _WebStream As System.IO.Stream = _WebResponse.GetResponseStream()


            _tmpImage = Image.FromStream(_WebStream)

            _WebResponse.Close()
            _WebStream.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return _tmpImage
    End Function

    Private Sub pb1_Click(sender As Object, e As EventArgs) Handles pb1.Click
        If pb1.Image IsNot Nothing Then
            MsgBox("Largo = " & pb1.Image.Width.ToString & vbCrLf & "Ancho = " & pb1.Image.Height.ToString)
        End If
    End Sub

    Private Sub btnParalle_Invoke_Click(sender As Object, e As EventArgs) Handles btnParalle_Invoke.Click
        txtDatos.Text = ""
        txtDatos.SuspendLayout()
        Parallel_Invoke()
        txtDatos.ResumeLayout()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub btnIP_Click(sender As Object, e As EventArgs) Handles btnIP.Click
        txtDatos.Text = ""
        Dim mensajes As String = ""
        mensajes &= "IP Privada (Solo intranet) = " & IPPrivada_DameLista(True) & vbCrLf
        mensajes &= "IP's Privadas (Todas) = " & IPPrivada_DameLista(False) & vbCrLf
        mensajes &= "IP's Privadas (Corto) = " & IPPrivada_DameCorto() & vbCrLf
        mensajes &= "IP Publica (Internet) = " & IPPublica_Dame() & vbCrLf & vbCrLf
        mensajes &= "userDomain = " & Environment.UserDomainName & vbCrLf
        mensajes &= "domain_user = " & WindowsIdentity.GetCurrent().Name & vbCrLf
        mensajes &= "computerDomain = " & WindowsIdentity.GetCurrent().User.Value
        txtDatos.Text = mensajes
    End Sub

    Private Sub btnCollections_Click(sender As Object, e As EventArgs) Handles btnCollections.Click
        ' DictionaryToString
        Dim dicPro As New Dictionary(Of String, String)
        dicPro.Add("cero", "0")
        dicPro.Add("uno", "1")
        dicPro.Add("dos", "2")
        dicPro.Add("tres", "3")
        dicPro.Add("cuatro", "4")
        dicPro.Add("cinco", "5")
        dicPro.Add("seis", "6")
        dicPro.Add("siete", "7")
        dicPro.Add("ocho", "8")
        dicPro.Add("nueve", "9")
        dicPro.Add("diez", "10")
        dicPro.Add("once", "11")
        dicPro.Add("doce", "12")
        dicPro.Add("trece", "13")
        '
        txtDatos.Text = "*** DictionaryToString ***" & vbCrLf
        txtDatos.Text = "dicPro (Keys=" & dicPro.Keys.Count & ", Values=" & dicPro.Values.Count & ")" & vbCrLf
        txtDatos.Text += "DictionaryToString = " & UtilesAlberto.Utiles.Dictionary_ToString(dicPro)
        txtDatos.Text += vbCrLf + vbCrLf
        '
        ' StringToDictionary
        Dim cadena = "cero·0|uno·1|dos·2|tres·3|cuatro·4|cinco·5|seis·6|siete·7|ocho·8|nueve·9|diez·10|once·11|doce·12|trece·13"
        Dim newDic As Dictionary(Of String, String) = UtilesAlberto.Utiles.String_ToDictionary(cadena)
        '
        txtDatos.Text += "*** StringToDictionary ***" & vbCrLf
        txtDatos.Text += "Origen=" + cadena & vbCrLf
        txtDatos.Text += "newDic (Keys=" & newDic.Keys.Count & ", Values=" & newDic.Values.Count & ")" + vbCrLf
        txtDatos.Text += UtilesAlberto.Utiles.Dictionary_Imprime(newDic) + vbCrLf
        txtDatos.Text += vbCrLf + vbCrLf
        '
        ' ArrayToString
        Dim arrPro() As String = {
            "cero·0·cm", "uno·1·mm", "dos·2·", "tres·3·cm", "cuatro·4·cm", "cinco·5·cm", "seis·6·cm",
            "siete·7·cm", "ocho·8·cm", "nueve·9·cm", "diez·10·kg", "once·11·", "doce·12·cm", "trece·13·mm"}
        '
        txtDatos.Text += "*** ArrayToString ***" & vbCrLf
        txtDatos.Text += "arrPro (Nº Items=" & arrPro.Count & ")" & vbCrLf
        txtDatos.Text += "ArrayToString = " & UtilesAlberto.Utiles.Array_ToString(arrPro)
        txtDatos.Text += vbCrLf + vbCrLf
        '
        ' StringToArray
        Dim cadena1 = "cero·0·cm|uno·1·mm|dos·2·|tres·3·cm|cuatro·4·cm|cinco·5·cm|seis·6·cm|"
        cadena1 += "siete·7·cm|ocho·8·cm|nueve·9·cm|diez·10·kg|once·11·|doce·12·cm|trece·13·mm"
        Dim newArr() As String = UtilesAlberto.Utiles.String_ToArray(cadena1)
        '
        txtDatos.Text += "*** StringToDictionary ***" & vbCrLf
        txtDatos.Text += "Origen=" + cadena1 & vbCrLf
        txtDatos.Text += "newArr (Nº Items=" & newArr.Count & ")" + vbCrLf
        txtDatos.Text += UtilesAlberto.Utiles.Array_Imprime(newArr) + vbCrLf
        txtDatos.Text += vbCrLf + vbCrLf
    End Sub
End Class
