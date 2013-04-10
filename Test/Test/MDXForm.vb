
Public Class MDXForm
    Public adminHost, server As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        Dim cat = CreateObject("ADOMD.Catalog")
        Dim cst = CreateObject("ADOMD.CellSet")
        Dim col_dim_count, row_dim_count, col_count, row_count As Integer
        Dim strSource As String


        strSource = RichTextBox1.Text


        Dim strNTServer As String, strTM1Server As String
        Dim strUser As String, strPassword As String, strConnection As String
        strNTServer = Me.adminHost
        strTM1Server = Me.server

        strUser = UserInfo.UserName
        strPassword = UserInfo.Password

        strConnection = "Provider=TM1OLAP;Location=" & strNTServer & ";Data Source=" & strTM1Server & ";User ID=" & strUser & ";Password=" & strPassword & ";"
        cat.ActiveConnection = strConnection
        cst.Source = strSource
        cst.ActiveConnection = cat.ActiveConnection
        cst.Open()

        col_dim_count = cst.Axes(0).Positions(0).Members.Count

        row_dim_count = cst.Axes(1).Positions(0).Members.Count

        row_count = cst.Axes(1).Positions.Count + col_dim_count

        col_count = cst.Axes(0).Positions.Count + row_dim_count

        For h = 1 To col_count
            DataForm.DataGridView1.Columns.Add(h.ToString, headerText:=h.ToString)
            If (h <= row_dim_count) Then

                DataForm.DataGridView1.Columns(h.ToString).Frozen = True
            End If
        Next

        DataForm.DataGridView1.Rows.Add(row_count - 1)


        For cur_row = 0 To row_count - 1


            DataForm.DataGridView1.Rows.Item(cur_row).HeaderCell.Value = (cur_row + 1).ToString

            For cur_col = 0 To col_count - 1

                If (cur_row < col_dim_count) Then

                    DataForm.DataGridView1.Rows(cur_row).Frozen = True

                    If (cur_col < row_dim_count) Then


                    Else

                        DataForm.DataGridView1.Rows(cur_row).Cells(cur_col).Value = cst.Axes(0).Positions(cur_col - row_dim_count).Members(cur_row).Caption

                    End If

                Else

                    If (cur_col < row_dim_count) Then

                        DataForm.DataGridView1.Rows(cur_row).Cells(cur_col).Value = cst.Axes(1).Positions(cur_row - col_dim_count).Members(cur_col).Caption


                    Else

                        DataForm.DataGridView1.Rows(cur_row).Cells(cur_col).Value = cst(cur_col - row_dim_count, cur_row - col_dim_count).FormattedValue

                    End If

                End If

            Next


        Next



        DataForm.Show()



    End Sub

    Public Sub New(ByVal serverName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.adminHost = serverName.Substring(0, serverName.IndexOf(":"))
        Me.server = serverName.Substring(serverName.IndexOf(":") + 1, serverName.Length - serverName.IndexOf(":") - 1)
        Me.Text = Me.server
    End Sub
End Class
