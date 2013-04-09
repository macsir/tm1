Option Explicit On
Imports Applix.TM1.API

Public Class Form2


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim Document, adminHosts As String

        Dim myfilestream As New  _
        System.IO.FileStream("AdminHostConfig.txt", _
        System.IO.FileMode.OpenOrCreate, _
        System.IO.FileAccess.ReadWrite, _
        System.IO.FileShare.None)



        Document = TextBox1.Text
        Dim mywriter As New System.IO.StreamWriter(myfilestream)
        mywriter.WriteLine(Document)
        mywriter.Close()
        myfilestream.Close()

        Dim i_ObjIdx As Integer, i_ObjCnt As Integer
        Dim objAdminSvr As TM1AdminServer
        Dim objServersCollection As TM1ServerInfoCollection
        'Dim MainForm As New Form1
        adminHosts = TextBox1.Text

        objAdminSvr = New TM1AdminServer(adminHosts, "tm1adminserver")
        objServersCollection = objAdminSvr.Servers
        i_ObjCnt = objServersCollection.Count

        For i_ObjIdx = 0 To i_ObjCnt - 1
            Form1.ServersToolStripMenuItem1.DropDownItems.Add(objServersCollection.Item(i_ObjIdx).Name)
        Next

        Form1.Show()
        Me.Hide()

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If System.IO.File.Exists("AdminHostConfig.txt") = True Then

            Dim myfilestream As New  _
            System.IO.FileStream("AdminHostConfig.txt", _
            System.IO.FileMode.OpenOrCreate, _
            System.IO.FileAccess.ReadWrite, _
            System.IO.FileShare.None)

            Dim myreader As New System.IO.StreamReader(myfilestream)
            TextBox1.Text = myreader.ReadToEnd
            myreader.Close()
            myfilestream.Close()

        End If



    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub
End Class