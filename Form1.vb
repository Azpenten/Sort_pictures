Imports System
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Text
Imports Microsoft.VisualBasic.FileIO
Public Class Form1
    Dim Files() As String
    Dim Dires() As String
    Dim Prop As PropertyItem
    Dim Photo As Image
    Dim NameTaken As String
    Dim DateTaken As String
    Dim Temp As String
    Dim Progress As Double
    Dim ProgressTemp As Double

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
            Files = Directory.GetFiles(TextBox1.Text)
            Dires = Directory.GetDirectories(TextBox1.Text)
            Label2.Text = Files.Length
            Label3.Text = Dires.Length
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Progress = ProgressBar1.Maximum / Files.Length
        For i As Integer = 0 To (Files.Length - 1)
            ProgressTemp += Progress
            ProgressBar1.Value = ProgressTemp
            Try
                Photo = Image.FromFile(Files(i))
                Prop = Photo.GetPropertyItem(&H9003)
            Catch ex As Exception
                Try
                    Photo.Dispose()
                Catch ex1 As Exception
                End Try
                Continue For
            End Try
            NameTaken = FileIO.FileSystem.GetName(Files(i))
            DateTaken = Encoding.UTF8.GetString(Prop.Value, 0, Prop.Value.Length).ToString
            Photo.Dispose()
            For j As Integer = 0 To (9)
                If (DateTaken.Chars(j) = ":") Then
                    Temp += "."
                Else
                    Temp += DateTaken.Chars(j)
                End If
            Next
            If (IO.Directory.Exists(TextBox1.Text + "\" + Temp) = False) Then
                My.Computer.FileSystem.CreateDirectory(TextBox1.Text + "\" + Temp)
            End If
            My.Computer.FileSystem.MoveFile(Files(i), TextBox1.Text + "\" + Temp + "\" + NameTaken)
            Temp = Nothing
        Next
        ProgressTemp = 0
        TextBox1_TextChanged(sender, e)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If (IO.Directory.Exists(TextBox1.Text) = True) Then
            Files = Directory.GetFiles(TextBox1.Text)
            Dires = Directory.GetDirectories(TextBox1.Text)
            Label2.Text = Files.Length
            Label3.Text = Dires.Length
            TextBox1.ForeColor = Color.Black
        Else
            TextBox1.ForeColor = Color.Red
        End If
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If (IO.Directory.Exists(TextBox1.Text) = True) Then
            Files = Directory.GetFiles(TextBox1.Text)
            Dires = Directory.GetDirectories(TextBox1.Text)
            Label2.Text = Files.Length
            Label3.Text = Dires.Length
        Else
            TextBox1.ForeColor = Color.Red
        End If
    End Sub

    Private Sub TextBox1_ForeColorChanged(sender As Object, e As EventArgs) Handles TextBox1.ForeColorChanged
        If TextBox1.ForeColor = Color.Red Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If

    End Sub
End Class
