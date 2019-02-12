Imports System.Net
Imports System.Net.Sockets
Imports System.Security.Principal
Imports System.Text
Imports System.Text.RegularExpressions
Imports IWshRuntimeLibrary
Imports UtilesAlberto

Public Class btnDuraction
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
        If btnDuraction.txtDatos.InvokeRequired Then
            Dim d As New PonTextoCallBack(AddressOf PonTexto)
            btnDuraction.Invoke(d, New Object() {btnDuraction.txtDatos.Text & text})
        Else
            btnDuraction.txtDatos.Text &= text
        End If
    End Sub

    Private Sub btnFtpUp_Click(sender As Object, e As EventArgs) Handles btnFtpUp.Click
        Dim dirLogsLocal As String = "C:\Users\alberto.ADA\AppData\Roaming\Autodesk\Revit\Addins\2018\UCRevit2018\logs"
        Dim dirLogsFTP As String = "ftp://ttu.ulmaconstruction.com/logs/Internal"
        ' Recorrer el directorio local y subir todos los ficheros
        For Each fiCsv As String In IO.Directory.GetFiles(dirLogsLocal, "*.csv", IO.SearchOption.TopDirectoryOnly)
            Dim nombre As String = IO.Path.GetFileName(fiCsv)
            Dim fiFtp As String = dirLogsFTP & "/" & nombre
            Dim resultadoTxt As String = UtilesAlberto.Utiles.FTP_Upload(fiCsv, fiFtp, FTP1_user, FTP1_pass)
            If resultadoTxt.StartsWith("ERROR") = False Then
                IO.File.Delete(fiCsv)
            End If
        Next
    End Sub

    Private Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        Dim Resultado As String = UtilesAlberto.Utiles.FTP_ListaDir(FTP1_dir, FTP1_user, FTP1_pass)
        If Resultado.ToUpper.StartsWith("ERROR") Then
            txtDatos.Text = "Falla la comunicación con el servidor"
        Else
            txtDatos.Text = FTP1_dir & " :" & vbCrLf & vbCrLf & String.Join(vbCrLf, Resultado)
        End If
    End Sub

    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        txtDatos.Text = ""
        Dim Resultado As String = UtilesAlberto.Utiles.FTP_ListaDir(FTP1_dir, FTP1_user, FTP1_pass)
        If Resultado.ToUpper.StartsWith("ERROR") Then
            txtDatos.Text = Resultado
        Else
            For Each fiCsv As String In String.Join(vbCrLf, Resultado)
                Dim fiFtp As String = FTP1_dir & "/" & fiCsv
                '
                If fiCsv.Contains(Environment.MachineName) = True Then
                    txtDatos.Text &= vbCrLf & UtilesAlberto.Utiles.FTP_Borra(fiFtp, FTP1_user, FTP2_pass)
                Else
                    txtDatos.Text &= vbCrLf & fiFtp & " no borrado."
                End If
            Next
        End If
    End Sub


    Private Sub btnExisteDir_Click(sender As Object, e As EventArgs) Handles btnExisteDir.Click
        txtDatos.Text = UtilesAlberto.Utiles.FTP_ExisteDir(FTP1_dir, FTP1_user, FTP1_pass) & vbCrLf & vbCrLf
        txtDatos.Text &= UtilesAlberto.Utiles.FTP_ExisteDir(FTP2_dir, FTP2_user, FTP2_pass)
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
        txtDatos.Text = "TASK" & vbCrLf
        Utiles.mTime_Inicio()
        txtDatos.SuspendLayout()
        Parallel_Task()
        txtDatos.ResumeLayout()
        System.Windows.Forms.Application.DoEvents()
        txtDatos.Text &= vbCrLf & Utiles.mTime_Duration & vbCrLf & vbCrLf
        '
        txtDatos.Text &= "PARALEL INVOKE" & vbCrLf
        Utiles.mTime_Inicio()
        txtDatos.SuspendLayout()
        Parallel_Invoke()
        txtDatos.ResumeLayout()
        System.Windows.Forms.Application.DoEvents()
        txtDatos.Text &= vbCrLf & Utiles.mTime_Duration
    End Sub

    Private Sub btnIP_Click(sender As Object, e As EventArgs) Handles btnIP.Click
        txtDatos.Text = ""
        Dim mensajes As String = ""
        mensajes &= "IP Privada (Solo intranet) = " & UtilesAlberto.Utiles.IPPrivada_DameLista(True) & vbCrLf
        mensajes &= "IP's Privadas (Todas) = " & UtilesAlberto.Utiles.IPPrivada_DameLista(False) & vbCrLf
        mensajes &= "IP's Privadas (Corto) = " & UtilesAlberto.Utiles.IPPrivada_DameCorto() & vbCrLf
        mensajes &= "IP Publica (Internet) = " & UtilesAlberto.Utiles.IPPublica_Dame() & vbCrLf & vbCrLf
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

    Private Sub btnDuracion_Click(sender As Object, e As EventArgs) Handles btnDuracion.Click
        Utiles.mTime_Inicio()
        Threading.Thread.Sleep(4250)
        Utiles.mTime_Duration(True)
    End Sub
End Class
