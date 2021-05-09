Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace UnitConversion
    Public Class Program
        Public Shared Sub Main()
            Dim values = New List(Of String)() From {
                "1000 mm",
                "20,2 cm",
                "3,35 m",
                "50 mm",
                "50,2 mm"
            }

            For Each value In values.AsQueryable().OrderBy(Function(v) GetAbsoluteValue(v))
                Console.WriteLine(value)
            Next
        End Sub

        Private Shared Function GetAbsoluteValue(ByVal v As String) As Object
            Dim powers = New Dictionary(Of String, Integer)() From {
                {"mm", 0},
                {"cm", 1},
                {"dm", 2},
                {"m", 3}
            }
            Dim numbersPattern = "([\d]*,?[\d]*)"
            ' unitPattern = (mm|cm|dm|m) -> all known units
            Dim unitPattern = String.Format("({0})", String.Join("|", powers.Keys))
            Dim number = Regex.Match(v, numbersPattern)
            Dim unit = Regex.Match(v, unitPattern)
            Dim result = 0.0
            ' set result to number only
            If number.Success Then result = Double.Parse(number.Value)
            ' overwrite result with converted value
            If number.Success AndAlso unit.Success AndAlso powers.ContainsKey(unit.Value) Then result = Double.Parse(number.Value) * Math.Pow(10, powers(unit.Value))
            Return result
        End Function
    End Class
End Namespace
