Option Explicit On
Imports Applix.TM1.API

Public Class LoginForm
    Public adminHost, server As String

    Private Sub MakeTree(ByVal objsDataServer As TM1Server, ByVal e As EventArgs)

        Dim i As Integer, j As Integer, k As Integer
        Dim strServer As String, strCube As String, strView As String
        Dim node As TreeNode

        'Get servers
        'For i = 1 To i_ObjCnt
        strServer = Me.server
        node = MainForm.TreeView1.Nodes.Add(strServer, strServer & " (" & UserInfo.UserName & ")", 9)
        node.SelectedImageIndex = 9

        'Get cubes
        For j = 1 To objsDataServer.Cubes.Count
            strCube = objsDataServer.Cubes.Item(j - 1).Name

            If strCube.Substring(0, 1) <> "}" Then
                node = MainForm.TreeView1.Nodes(0).Nodes.Add(strCube, strCube, 4)
                node.SelectedImageIndex = 4

                'Get cube public views
                For k = 1 To objsDataServer.Cubes.Item(j - 1).PublicViews.Count
                    strView = objsDataServer.Cubes.Item(j - 1).PublicViews.Item(k - 1).Name
                    node = MainForm.TreeView1.Nodes(0).Nodes(j - 1).Nodes.Add(strView, strView, 7)
                    node.SelectedImageIndex = 7
                Next k

                For k = 1 To objsDataServer.Cubes.Item(j - 1).PrivateViews.Count
                    strView = objsDataServer.Cubes.Item(j - 1).PrivateViews.Item(k - 1).Name
                    node = MainForm.TreeView1.Nodes(0).Nodes(j - 1).Nodes.Add(strView, strView, 8)
                    node.SelectedImageIndex = 8
                Next k

            End If
        Next j
        'End If
        'Next i

        MainForm.TreeView1.Nodes(0).Expand()



    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim i_ObjCnt As Integer

        Dim objAdminSvr As TM1AdminServer
        Dim objServersCollection As TM1ServerInfoCollection
        Dim objServerInfo As TM1ServerInfo
        Dim objsDataServer As TM1Server
        Dim userName, password As String

        objAdminSvr = New TM1AdminServer(Me.adminHost, "tm1adminserver")
        objServersCollection = objAdminSvr.Servers
        i_ObjCnt = objServersCollection.Count

        objServerInfo = objServersCollection.Item(Me.server)
        userName = Me.TextBox1.Text
        password = Me.TextBox2.Text
        UserInfo.UserName = userName
        UserInfo.Password = password
        objsDataServer = objServerInfo.Login(userName, password)

        MakeTree(objsDataServer, e)

        MainForm.RunMDXToolStripMenuItem.Enabled = True
        Me.Dispose()
    End Sub

    Public Sub New(ByVal serverName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.adminHost = serverName.Substring(0, serverName.IndexOf(":"))
        Me.server = serverName.Substring(serverName.IndexOf(":") + 1, serverName.Length - serverName.IndexOf(":") - 1)
        Me.Text = Me.server

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Dispose()
    End Sub
End Class