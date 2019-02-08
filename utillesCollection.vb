Imports System.Xml
Imports System.Xml.Linq
Imports System.Linq
Module utillesCollection
    Public Const sepItems As String = "·"
    Public Const sepValues As String = "|"
    Public Function Dictionary_ToString(Dic As Dictionary(Of String, String), Optional sItems As String = sepItems, Optional sValues As String = sepValues) As String
        Dim resultado As String = ""
        Dim union = From k In Dic.Keys
                    Select k & sItems & Dic(k)

        resultado = String.Join(sValues, union.ToArray)
        Return resultado
    End Function

    Public Function String_ToDictionary(Str As String, Optional sItems As String = sepItems, Optional sValues As String = sepValues) As Dictionary(Of String, String)
        Dim resultado As New Dictionary(Of String, String)
        Dim arrValues As String() = Str.Split(sValues)
        For Each Value In arrValues
            Dim arrItems() As String = Value.Split(sItems)
            If arrItems.Count = 2 Then
                resultado.Add(arrItems(0), arrItems(1))
            ElseIf arrItems.Count = 1 Then
                resultado.Add(arrItems(0), "")
            End If
        Next
        '
        Return resultado
    End Function

    Public Function Array_ToString(Arr As String(), Optional sItems As String = sepItems, Optional sValues As String = sepValues) As String
        Dim resultado As String = ""
        resultado = String.Join(sValues, Arr)
        Return resultado
    End Function

    Public Function String_ToArray(Str As String, Optional sItems As String = sepItems, Optional sValues As String = sepValues) As String()
        Dim resultado As New List(Of String)
        Dim arrValues As String() = Str.Split(sValues)
        For Each Value In arrValues
            Dim arrItems() As String = Value.Split(sItems)
            resultado.Add(String.Join(sItems, arrItems))
        Next
        '
        Return resultado.ToArray
    End Function

    Public Function Dictionary_Imprime(Dic As Dictionary(Of String, String), Optional sItems As String = sepItems) As String
        Dim resultado As String = ""
        Array.ForEach(Dic.Keys.ToArray, Sub(a As String)
                                            resultado &= a & sepItems & Dic(a) & vbCrLf
                                        End Sub)
        Return resultado
    End Function
    Public Function Array_Imprime(Arr As String(), Optional sItems As String = sepItems) As String
        Dim resultado As String = ""
        Array.ForEach(Arr, Sub(a As String)
                               resultado &= String.Join(sepItems, a) & vbCrLf
                           End Sub)
        Return resultado
    End Function
End Module
