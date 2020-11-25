'David Harmon
'RCET0265
'Spring 2020
'EtchOSketch
'https://github.com/harmdavi/EtchOSketch4.git
Public Class EtchOSketch4
    Dim LastX As Integer
    Dim LastY As Integer
    Dim CurrentX As Integer
    Dim CurrentY As Integer
    Dim PenColor As Color
    Dim WidthDU As Integer
    Dim HeightDU As Integer
    Dim VP As Integer
    Dim TimeStep As Integer
    Dim DataPoint As Integer
    Dim LastTime As Integer
    Dim LastAmp As Integer
    Sub Drawline()
        'This declares the variable g as a graphics type and also the pen that will be used.
        Dim g As Graphics = PictureBox1.CreateGraphics
        Dim pen As New Pen(PenColor)
        g.DrawLine(pen, LastX, LastY, CurrentX, CurrentY)
        pen.Dispose()
        g.Dispose()
        'Disposing of the pen is very efficent for the processor so it doesnt have to keep on running it while the program runs
    End Sub
    Private Sub EtchOSketch4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'This defaults the pen color to black.
        PenColor = Color.Black
    End Sub
    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        'This handels any conditions that would happen as the mouse is dragged inside of the picture box.
        CurrentY = e.Y
        CurrentX = e.X
        'if the left mouse button is pressed then this if stataement will activate and allow for a line to be drawn by 
        'the use of the drawline function. 
        If e.Button.ToString = "Left" Then
            Drawline()
            ' this controls the right mouse button click. when pressed it will show the color dialog panel to choose the pen color
        ElseIf e.Button.ToString = "Right" Then
            ColorDialog1.ShowDialog()
            PenColor = ColorDialog1.Color
        End If
        'this remembers the last x and y coordinate and continues the line fromt he last postion (keep in mind that this sub is 
        'active during any time that the mouse cursor is on the picturebox.)
        EtchOSketch4.ActiveForm.Text = CStr(LastX & "      " & LastY)
        LastX = e.X
        LastY = e.Y
    End Sub
    Private Sub SelectColorButton_Click(sender As Object, e As EventArgs) Handles SelectColorButton.Click
        'This allows the user to choose the color that is being drawn in the picture box
        ColorDialog1.ShowDialog()
        PenColor = ColorDialog1.Color
    End Sub
    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        'This button will clear any lines drawn with the pen or the waveform button. 
        PictureBox1.Image = Nothing
    End Sub
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        'this exits the program
        Me.Close()
    End Sub
    Private Sub DrawWaveformsButton_Click(sender As Object, e As EventArgs) Handles DrawWaveformsButton.Click
        'After much effort of trying to figure out how to scale this on my own. I have used The code devised by TTim Rossiter to
        'solve this part of the code. This part of the code draws a waveform across the etch o sketch.
        Dim xCurrent As Double
        Dim xMax As Single = 360 'This will be the width of the scaled graphics object 
        Dim xOld As Double
        Dim xScale As Single = CSng(PictureBox1.Width / xMax)
        Dim xOffset As Single
        Dim yCurrent As Double
        Dim yMax As Single = 100 'This will be the height of the scaled graphics object 
        Dim yOld As Double
        Dim yScale As Single = CSng(((PictureBox1.Height) / 2) / yMax)
        Dim yOffset As Single = CSng(yMax)
        Dim numberOfPoints As Integer = 360 'This will be the resolution of the drawn shape
        Dim xPerPoints As Integer = CInt(xMax / numberOfPoints)
        For xCurrent = 0 To xMax Step xPerPoints
            yCurrent = yMax * Math.Sin((Math.PI / 180) * xCurrent) * -1
            Try
                DrawStuff(CInt(xOld), CInt(yOld), CInt(xCurrent), CInt(yCurrent), xScale, yScale, xOffset, yOffset, 0)

                xOld = xCurrent
                yOld = yCurrent
            Catch
            End Try
        Next
        'this part of the code sizes the picture box so that the code knows where to draw the waveform. 
        WidthDU = PictureBox1.Size.Width
        HeightDU = PictureBox1.Size.Height
        VP = HeightDU - 10
        TimeStep = WidthDU / 150
        'The provides the data points needed to create a sine wave. 
        For i = 1 To 150
            TimeStep = (Width / 150) = +TimeStep
            DataPoint = VP * Math.Sin((180 * Math.PI) * 360 * (1 / WidthDU) * (TimeStep)) - (HeightDU / 2)
            Dim g As Graphics = PictureBox1.CreateGraphics
            Dim pen As New Pen(PenColor)
            g.DrawLine(pen, LastTime, LastAmp, TimeStep, DataPoint)
            pen.Dispose()
            g.Dispose()
            LastTime = TimeStep
            LastAmp = DataPoint
        Next
    End Sub
    Sub DrawStuff(ByRef x1 As Integer, ByRef y1 As Integer,
                 ByRef x2 As Integer, ByRef y2 As Integer,
                 ByRef sx As Single, ByRef sy As Single,
                 ByRef dx As Single, ByRef dy As Single,
                 ByRef R As Single)
        Dim g As Graphics = PictureBox1.CreateGraphics
        Dim pen As New Pen(PenColor)
        g.PageUnit = GraphicsUnit.Pixel
        g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighSpeed
        g.ScaleTransform(sx, sy)
        g.TranslateTransform(dx, dy)
        g.RotateTransform(R)
        g.DrawLine(pen, x1, y1, x2, y2)
        pen.Dispose()
        g.Dispose()
    End Sub
End Class
