Imports System.Text.RegularExpressions

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(TextBox1.Text)
        Dim response As System.Net.HttpWebResponse = request.GetResponse

        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())

        Dim rssourcecode As String = sr.ReadToEnd


        'rssourcecode = Regex.Replace(rssourcecode, "<!(.|\s)*?>", "")
        'href="magnet:?xt=urn:btih:52bda2d7e89b0a60931a5732e38768474aca8bff&amp;dn=Castle+2009+S02E01+HDTV+XviD-2HD+%5Beztv%5D&amp;tr=udp%3A%2F%2Ftracker.openbittorrent.com%3A80&amp;tr=udp%3A%2F%2Fopen.demonii.com%3A1337&amp;tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&amp;tr=udp%3A%2F%2Fexodus.desync.com%3A6969"

        '  Dim r = New Regex("magnet:?xt=urn:btih:.*")
        'Dim r = New Regex("<a style=.*background-image: url.*>") href=.*
        Dim r = New Regex("magnet:.*btih.*%3A6969")
        Dim matches As MatchCollection = r.Matches(rssourcecode)
        'If matches.Count.Equals(0) Then
        'Else
        '    ListBox2.Items.Add(matches(1))
        'End If
        Try
            ListBox2.Items.Add(matches(1))

        Catch Exc As System.ArgumentOutOfRangeException
            MsgBox("You probably entered too many episodes or it just can't be found on thepiratebay.la. Just keep clicking OK")
        End Try
        'For Each url As Match In matches
        '    ListBox1.Items.Add(url)
        '    TextBox2.AppendText(url.ToString)
        'Next
        'For Each url As Match In matches
        '    ListBox1.Items.Add(url)
        '    'MsgBox(url)
        'Next

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        'ListBox1.Items.Add("test")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MsgBox("Please Wait this can take a while if you have a large range")
        TextBox2.Text.Replace(" ", "%20")
        Dim x As Integer
        x = NumericUpDown1.Value
        Dim xs As String

        Dim y As Integer
        y = NumericUpDown3.Value

        Dim ys As String
        Do While x <= NumericUpDown1.Value
            Do While y <= NumericUpDown2.Value
                If x <= 9 Then
                    xs = "0" + x.ToString
                Else
                    xs = x.ToString
                End If
                If y <= 9 Then
                    ys = "0" + y.ToString
                Else
                    ys = y.ToString
                End If
                TextBox1.Text() = "http://thepiratebay.la/lucky/" + TextBox2.Text + "%20S" + xs + "E" + ys + "/"
                Button1.PerformClick()

                y = y + 1
                'Threading.Thread.Sleep(500)
            Loop
            x = x + 1
        Loop
        Button3.Enabled = True

    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs)

    End Sub

    Private Sub ToolTip1_Popup_1(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolTip1.SetToolTip(TextBox2, "Set a TV Show Name such as Castle 2009")

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim w As IO.StreamWriter
        w = New IO.StreamWriter("export.txt")
        For i = 0 To ListBox2.Items.Count - 1
            w.WriteLine(ListBox2.Items.Item(i))
        Next
        w.Flush()
        w.Close()
    End Sub

    Private Sub CreateHelpProvider()
        Dim hlpProvider As HelpProvider
        hlpProvider = New System.Windows.Forms.HelpProvider()
        hlpProvider.SetShowHelp(Button3, "This file will be exported as a txt file in the same directory as this program")
    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown3.ValueChanged
        If NumericUpDown3.Value > NumericUpDown2.Value Then
            NumericUpDown3.Value = NumericUpDown2.Value
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        If NumericUpDown3.Value > NumericUpDown2.Value Then
            NumericUpDown3.Value = NumericUpDown2.Value
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://github.com/ndragon798")
    End Sub
End Class
