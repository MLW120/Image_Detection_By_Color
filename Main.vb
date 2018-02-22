Public Class Form1
    Public Pix(400, 300) As Pixels
    Public Dic As SortedDictionary(Of Integer, Integer)
    Public Col As New Color

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dic = New SortedDictionary(Of Integer, Integer)
        AddEntrada(1, 1)
        AddEntrada(2, 2)
        AddEntrada(3, 3)
        AddEntrada(3, 1)
        AddEntrada(4, 2)
        AddEntrada(4, 3)
        AddEntrada(5, 5)
        ArreglarEntradas()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim BM As New Bitmap(PictureBox1.BackgroundImage)
        NewPixels(BM)
        Dim BM2 As Bitmap
        BM2 = DinsColor(BM)
        'MsgBox("")
        Ventanas(BM2)
        'MsgBox("")
    End Sub

    Private Sub NewPixels(ByVal BM As Bitmap)
        For i As Integer = 0 To BM.Width - 1
            For j As Integer = 0 To BM.Height - 1
                'ReDim Preserve Pix(i, j) 'Que pasa si la posicio de la matriu ja existeix? Que pasa amb el preserve???
                Pix(i, j) = New Pixels
                Pix(i, j).Color = BM.GetPixel(i, j)
                Pix(i, j).InColor = False
                Pix(i, j).Objecte = 0
            Next
        Next
    End Sub

    Private Function DinsColor(ByVal BM As Bitmap) As Bitmap
        Dim BMNew As Bitmap
        BMNew = BM
        For i As Integer = 0 To BM.Width - 1
            For j As Integer = 0 To BM.Height - 1
                Pix(i, j).InBounds()
                If Pix(i, j).InColor Then
                    BMNew.SetPixel(i, j, Color.White)
                Else
                    BMNew.SetPixel(i, j, Color.Black)
                End If
            Next
        Next
        Dim im As Image = BMNew
        PictureBox2.BackgroundImage = im
        Return BMNew
    End Function

    Private Sub Ventanas(ByVal BM As Bitmap)
        Dim Objectes As Integer = 1
        Dic = New SortedDictionary(Of Integer, Integer)
        For i As Integer = 0 To BM.Width - 1
            For j As Integer = 0 To BM.Height - 1
                If BM.GetPixel(i, j).R = 255 Then

                    Dim count As Integer = 0
                    Dim objecteLocal1 As Integer = 0
                    Dim objecteLocal2 As Integer = 0

                    If i = 0 Or j = 0 Then
                        If i = 0 Then
                            If j = 0 Then
                                count = 0
                            Else
                                If BM.GetPixel(i, j - 1) = Color.White Then count = 1 And objecteLocal1 = Pix(i - 1, j).Objecte
                            End If
                        End If
                        If j = 0 Then
                            If i <> 0 Then
                                If BM.GetPixel(i - 1, j) = Color.White Then count = 1 And objecteLocal1 = Pix(i, j - 1).Objecte
                            End If
                        End If
                    Else
                        If BM.GetPixel(i - 1, j).R = 255 Then
                            count += 1
                            objecteLocal1 = Pix(i - 1, j).Objecte
                        End If
                        'If BM.GetPixel(i - 1, j - 1) = Color.White Then count += 1
                        If BM.GetPixel(i, j - 1).R = 255 Then
                            count += 1
                            objecteLocal2 = Pix(i, j - 1).Objecte
                        End If
                        'If BM.GetPixel(i + 1, j - 1) = Color.White Then count += 1
                    End If

                    Select Case count
                        Case 0
                            Pix(i, j).Objecte = Objectes
                            Objectes += 1
                        Case 1
                            If objecteLocal1 = 0 Then
                                Pix(i, j).Objecte = objecteLocal2
                            Else
                                Pix(i, j).Objecte = objecteLocal1
                            End If
                        Case 2
                            Pix(i, j).Objecte = objecteLocal1
                            Dim numS As Integer
                            Dim numL As Integer
                            If objecteLocal1 < objecteLocal2 Then 'He canviat el signe de equivalencia perque el key sigui el gran i sigui mes facil la substitucio
                                numL = objecteLocal2
                                numS = objecteLocal1
                            Else
                                numL = objecteLocal1
                                numS = objecteLocal2
                            End If
                            If numL <> numS Then 'Si son iguals ja sha afegit un dels dos i es pot sortir. No hi ha equivalencia.

                                AddEntrada(numL, numS)

                            End If

                    End Select
                End If ' No white
            Next j
        Next i
        ArreglarEntradas()

        Dim BMNew As New Bitmap(PictureBox2.BackgroundImage)

        For i As Integer = 0 To BM.Width - 1
            For j As Integer = 0 To BM.Height - 1
                If Pix(i, j).Objecte <> 0 Then
                    If Dic.ContainsKey(Pix(i, j).Objecte) Then
                        Pix(i, j).Objecte = Dic(Pix(i, j).Objecte)
                        If Pix(i, j).Objecte = 1 Then
                            BMNew.SetPixel(i, j, Color.Blue)
                        Else
                            BMNew.SetPixel(i, j, Color.Red)
                        End If
                    Else
                        If Pix(i, j).Objecte = 1 Then
                            BMNew.SetPixel(i, j, Color.Blue)
                        Else
                            BMNew.SetPixel(i, j, Color.Red)
                        End If
                    End If

                End If
            Next
        Next

        Dim im As Image = BMNew
        PictureBox3.BackgroundImage = im

        Dim zones As New SortedDictionary(Of Integer, Integer)
        Dim inte As Integer = 1
        For Each line In Dic
            If zones.ContainsValue(line.Value) Then

            Else
                zones.Add(inte, line.Value)
                inte += 1
            End If
        Next

        For Each line In zones

        Next

    End Sub


    Private Sub AddEntrada(ByVal x As Integer, ByVal z As Integer)
        If Not Dic.ContainsKey(x) Then
            Dic.Add(x, z)
            Exit Sub
        Else
            Dim y As Integer = Dic(x)
            If y = x Then
                Dic(x) = z
                Exit Sub
            End If
            If y = z Then Exit Sub
            If y > z Then
                AddEntrada(y, z)
            Else
                Dic(x) = z
                AddEntrada(z, y)
            End If
        End If
    End Sub

    Private Sub ArreglarEntradas()
        Dim DicTemp As New SortedDictionary(Of Integer, Integer)
        For Each Line As KeyValuePair(Of Integer, Integer) In Dic
            DicTemp.Add(Line.Key, Line.Value)
        Next
        For Each Line As KeyValuePair(Of Integer, Integer) In Dic
            If DicTemp.ContainsKey(Line.Value) Then
                DicTemp(Line.Key) = DicTemp(Line.Value)
            End If
        Next
        Dic = New SortedDictionary(Of Integer, Integer)
        For Each Line As KeyValuePair(Of Integer, Integer) In DicTemp
            Dic.Add(Line.Key, Line.Value)
        Next
    End Sub

    Private Sub TrackBar1_Scroll(sender As System.Object, e As System.EventArgs) Handles TrackBar1.Scroll
        Label4.Text = TrackBar1.Value
    End Sub

    Private Sub TrackBar2_Scroll(sender As System.Object, e As System.EventArgs) Handles TrackBar2.Scroll
        Label5.Text = TrackBar2.Value
    End Sub

    Private Sub TrackBar3_Scroll(sender As System.Object, e As System.EventArgs) Handles TrackBar3.Scroll
        Label6.Text = TrackBar3.Value
    End Sub

    Private Sub PictureBox1_DoubleClick(sender As Object, e As System.EventArgs) Handles PictureBox1.DoubleClick
        If CheckBox1.Checked = True Then
            Dim MouseE As MouseEventArgs = e
            Dim x As Integer = MouseE.X
            Dim y As Integer = MouseE.Y
            x -= 68
            Dim bm As New Bitmap(PictureBox1.BackgroundImage)
            Col = bm.GetPixel(x, y)
            Label7.Text = Col.R
            Label8.Text = Col.G
            Label9.Text = Col.B
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Col = Color.FromArgb(255, 200, 200, 200)
    End Sub
End Class
