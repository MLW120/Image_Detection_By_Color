Public Class Pixels
    Public Color As Color
    Public InColor As Boolean
    Public Objecte As Integer

    Public Sub InBounds()
        Dim R As Integer = Me.Color.R
        Dim G As Integer = Me.Color.G
        Dim B As Integer = Me.Color.B
        Dim RCentre As Integer = Form1.Col.R '200 '117.5 '165 '200
        Dim GCentre As Integer = Form1.Col.G '200  '66.0 '161 '180
        Dim BCentre As Integer = Form1.Col.B '200  '38.5 '150 '180
        Dim RLong As Integer = Form1.TrackBar1.Value '155 '113
        Dim GLong As Integer = Form1.TrackBar2.Value '155  '80
        Dim BLong As Integer = Form1.TrackBar3.Value '155  '70
        Dim InR As Boolean = False
        Dim InG As Boolean = False
        Dim InB As Boolean = False
        If R > (RCentre - RLong / 2) And R < (RCentre + RLong / 2) Then InR = True
        If G > (GCentre - GLong / 2) And G < (GCentre + GLong / 2) Then InG = True
        If B > (BCentre - BLong / 2) And B < (BCentre + BLong / 2) Then InB = True
        If InR And InG And InB Then Me.InColor = True
    End Sub
End Class
