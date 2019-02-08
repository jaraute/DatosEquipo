Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Module utilesVB
    Public Function IPPrivada_Dame(Optional solointranet As Boolean = True) As String
        Dim valorIp As String = ""
        If solointranet Then
            valorIp = Dns.GetHostEntry(My.Computer.Name).AddressList.FirstOrDefault(
                Function(i) i.AddressFamily = Sockets.AddressFamily.InterNetwork).ToString()
        Else
            Dim Direcciones As IPAddress() = Dns.GetHostEntry(My.Computer.Name).AddressList
            'Se despliega la lista de IP's
            For i = 0 To Direcciones.Length - 1
                valorIp &= "IP " & (i + 1) & ": " & Direcciones(i).ToString() + vbCrLf
                If i < Direcciones.Length - 1 Then
                    valorIp &= vbCrLf
                End If
            Next
        End If
        Return valorIp
    End Function
    Public Function IPPrivada_DameLista(Optional solointranet As Boolean = True) As String
        Dim valorIp As String = ""
        Dim listAddres = Dns.GetHostEntry(My.Computer.Name).AddressList
        If solointranet Then
            valorIp = listAddres.FirstOrDefault(
                Function(i) i.AddressFamily = Sockets.AddressFamily.InterNetwork).ToString()
        Else
            Dim varios = From i In listAddres
                         Where i.AddressFamily = Sockets.AddressFamily.InterNetwork
                         Select i

            valorIp = String.Join("|", varios.Cast(Of String).ToArray)
        End If
        Return valorIp
    End Function
    Function IPPublica_Dame() As String  ' IPAddress
        Dim resultado As String = ""
        Dim lol As WebClient = New WebClient()
        Try
            Dim str As String = lol.DownloadString("http://checkip.dyndns.org/")    ' El html con el resultado
            resultado = str.Split(":"c)(1).Split("<"c)(0).Trim
        Catch ex As Exception
            ' Error de conexión, no hay internet o la página no ha dado la IP
            resultado = "¿?"
        End Try

        Return resultado
    End Function
End Module
