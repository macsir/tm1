
Imports Excel = Microsoft.Office.Interop.Excel



Public Class Form2

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Export DataGridView to Excel
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add
        xlWorkSheet = xlWorkBook.Sheets("Sheet1")

        For i = 0 To Me.DataGridView1.RowCount - 1
            For j = 0 To Me.DataGridView1.ColumnCount - 1
                If (Me.DataGridView1.Rows(i).Cells(j).Value Is vbNullString) Then
                    xlworksheet.Cells(i + 1, j + 1).Value = ""
                Else
                    xlworksheet.Cells(i + 1, j + 1).Value = Me.DataGridView1.Rows(i).Cells(j).Value.ToString
                End If

            Next
        Next
        Dim dialog As New SaveFileDialog

        dialog.Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls|CSV (Comma delimited)|*.csv"
        dialog.ShowDialog()
        If dialog.FileName <> "" Then
            xlWorkSheet.SaveAs(dialog.FileName)

        End If

        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)

    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

End Class