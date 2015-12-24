Imports System.Text.RegularExpressions

Public Class Form1
    Dim sup As Boolean
    Dim searches As New ArrayList
    Dim result3 As DialogResult



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
        Dim rn = New Regex("id.*title.*\n.*<.*")
        ' Dim rn = New Regex("id.*title.*\n        .*<.*div>")
        Dim matchesn As MatchCollection = rn.Matches(rssourcecode)
        Dim matches As MatchCollection = r.Matches(rssourcecode)
        'If matches.Count.Equals(0) Then
        'Else
        '    ListBox2.Items.Add(matches(1))
        'End If
        Dim matsn As String
        matsn = ""
        Try
            matsn = matchesn(1).ToString.Substring(20)
            matsn = matsn.Substring(0, matsn.Length - 6)
        Catch ex As Exception
            'MsgBox("MATSN ERROR")
        End Try
        Try
            'ListBox2.Items.Add("S" + xs + "E" + ys)
            If matsn.Equals("") Then
            Else
                ListBox2.Items.Add(matsn)

            End If

            ListBox2.Items.Add(matches(1))
        Catch Exc As System.ArgumentOutOfRangeException
            If sup.Equals(False) Then
                MsgBox("You probably miss spelled the name or its just not found.")
                ListBox2.Items.Add("S" + xs + "E" + ys)
                ListBox2.Items.Add("ERROR")
                sup = True
            Else
                ListBox2.Items.Add("S" + xs + "E" + ys)
                ListBox2.Items.Add("ERROR")
            End If


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

    Dim x As Integer
    Dim xs As String

    Dim y As Integer
    Dim ys As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (searches.Contains(TextBox2.Text)) Then

        Else
            searches.Add(TextBox2.Text)
        End If


        sup = False
        MsgBox("Please Wait this can take a while if you have a large range")

        TextBox2.Text.Replace(" ", "%20")

        x = NumericUpDown1.Value

        y = NumericUpDown3.Value

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


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox3.Text = ""
        TextBox3.AppendText("<html>")
        TextBox3.AppendText("<head>")
        TextBox3.AppendText(" <title>Magnet Scraper</title>")
        TextBox3.AppendText("<b>")
        For sear = 0 To searches.Count - 1
            TextBox3.AppendText(searches(sear))
            If sear < searches.Count - 1 Then
                TextBox3.AppendText(", ")
            End If

        Next
        TextBox3.AppendText("</b>")
        TextBox3.AppendText("<br>")
        'TextBox3.AppendText(vbNewLine)


        For wr = 1 To ListBox2.Items.Count - 1 Step 2
            TextBox3.AppendText("<a href=""" + ListBox2.Items.Item(wr).ToString + """>" + ListBox2.Items(wr - 1).ToString + "</a>")
            TextBox3.AppendText(vbNewLine)
            TextBox3.AppendText("<br>")
            'TextBox3.AppendText(vbNewLine)
            'TextBox3.AppendText(ListBox2.Items.Item(wr).ToString)
        Next
        TextBox3.AppendText("</html>")
        SaveFileDialog1.Filter = "HTML Files (*.html)|*.html"

   
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox3.Text, True)
            Process.Start(SaveFileDialog1.FileName)
        End If
        'Dim w As IO.StreamWriter
        'w = New IO.StreamWriter("export.txt")
        'For i = 0 To ListBox2.Items.Count - 1
        '    w.WriteLine(ListBox2.Items.Item(i))
        'Next
        'w.Flush()
        'w.Close()
        Button3.Enabled = False
    End Sub

    Private Sub CreateHelpProvider()

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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ListBox2.Items.Clear()
        TextBox3.Clear()
        searches.Clear()
        TextBox2.Clear()
        TextBox1.Clear()

        'Button4.Enabled = False
    End Sub

End Class
