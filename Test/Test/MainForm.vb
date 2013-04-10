Option Explicit On
Imports Applix.TM1.API


Public Class MainForm
    Public selectedServer As String

    Private Sub ServersSubMenuItemClicked(ByVal sender As Object, ByVal e As EventArgs)

        Dim ServerName As String
        Dim clickedItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

        clickedItem.Checked = True
        ServerName = clickedItem.Text
        selectedServer = clickedItem.Text
        Dim LF As New LoginForm(ServerName)
        LF.Show()

    End Sub


    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.Text = "TM1 MDX Player 1.0"
        Dim adminHosts As String

        If System.IO.File.Exists("AdminHostConfig.txt") = True Then

            Dim myfilestream As New  _
            System.IO.FileStream("AdminHostConfig.txt", _
            System.IO.FileMode.OpenOrCreate, _
            System.IO.FileAccess.ReadWrite, _
            System.IO.FileShare.None)

            Dim myreader As New System.IO.StreamReader(myfilestream)
            adminHosts = myreader.ReadToEnd
            myreader.Close()
            myfilestream.Close()

            Dim i_ObjIdx As Integer, i_ObjCnt As Integer
            Dim objAdminSvr As TM1AdminServer
            Dim objServersCollection As TM1ServerInfoCollection

            objAdminSvr = New TM1AdminServer(adminHosts, "tm1adminserver")
            objServersCollection = objAdminSvr.Servers
            i_ObjCnt = objServersCollection.Count

            For i_ObjIdx = 0 To i_ObjCnt - 1
                Me.ServersToolStripMenuItem1.DropDownItems.Add(objServersCollection.Item(i_ObjIdx).HostName & ":" & objServersCollection.Item(i_ObjIdx).Name)
                AddHandler Me.ServersToolStripMenuItem1.DropDownItems.Item(i_ObjIdx).Click, AddressOf ServersSubMenuItemClicked
            Next

            objAdminSvr.Dispose()
            objServersCollection.Dispose()
        End If

        Me.RunMDXToolStripMenuItem.Enabled = False

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        Dim i_ObjIdx As Integer, i_ObjCnt As Integer

        Dim objAdminSvr As TM1AdminServer
        Dim objServersCollection As TM1ServerInfoCollection
        Dim objServerInfo As TM1ServerInfo
        Dim objsDataServer As TM1Server
        Dim objTM1Process As TM1Process
        Dim objaParams(1) As TM1ProcessParameter
        Dim test As String

        Try
            test = "na-dev-cognos.nalan.gu.edu.au"
            objAdminSvr = New TM1AdminServer(test, "tm1adminserver")


            objServersCollection = objAdminSvr.Servers

            Debug.Print("The count of servers is " & CType(objServersCollection.Count, String))

            i_ObjCnt = objServersCollection.Count

            Debug.Print("List of servers")
            Debug.Print("-------------------------")

            For i_ObjIdx = 0 To i_ObjCnt - 1
                Debug.Print(objServersCollection.Item(i_ObjIdx).Name)
            Next

            Debug.Print(vbNewLine)

            objServerInfo = objServersCollection.Item("Griffith-Dev")

            objsDataServer = objServerInfo.Login("morganm", "pass123")



            Dim i As Integer, j As Integer, k As Integer
            Dim strServer As String, strCube As String, strView As String
            Dim node As TreeNode

            'Get servers
            For i = 1 To i_ObjCnt
                strServer = objServersCollection.Item(i - 1).Name
                node = Me.TreeView1.Nodes.Add(strServer, strServer, 0)
                node.SelectedImageIndex = 0

                'Get cubes
                For j = 1 To objsDataServer.Cubes.Count
                    strCube = objsDataServer.Cubes.Item(j - 1).Name

                    If strCube.Substring(0, 1) <> "}" Then
                        node = Me.TreeView1.Nodes(i - 1).Nodes.Add(strCube, strCube, 1)
                        node.SelectedImageIndex = 1

                        'Get cube public views
                        For k = 1 To objsDataServer.Cubes.Item(j - 1).PublicViews.Count
                            strView = objsDataServer.Cubes.Item(j - 1).PublicViews.Item(k - 1).Name
                            node = Me.TreeView1.Nodes(i - 1).Nodes(j - 1).Nodes.Add(strView, strView, 2)
                            node.SelectedImageIndex = 2
                        Next k

                        For k = 1 To objsDataServer.Cubes.Item(j - 1).PrivateViews.Count
                            strView = objsDataServer.Cubes.Item(j - 1).PrivateViews.Item(k - 1).Name
                            node = Me.TreeView1.Nodes(i - 1).Nodes(j - 1).Nodes.Add(strView, strView, 3)
                            node.SelectedImageIndex = 3
                        Next k

                    End If
                Next j
                'End If
            Next i

            Me.TreeView1.Nodes(0).Expand()




        Catch ex As Exception
            MessageBox.Show("Failed to complete the example. Error " _
             & ex.ToString, "TM1 Dot Net API Example", MessageBoxButtons.OK, _
             MessageBoxIcon.Error)
        End Try

        Try

            'the presence of a Dispose() method so if it's there, I'll use it.
            objTM1Process.Dispose()
            objsDataServer.Dispose()
            objServerInfo.Dispose()
            objServersCollection.Dispose()
            objAdminSvr.Dispose()

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ConnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i_ObjIdx As Integer, i_ObjCnt As Integer

        Dim objAdminSvr As TM1AdminServer
        Dim objServersCollection As TM1ServerInfoCollection
        Dim objServerInfo As TM1ServerInfo
        Dim objsDataServer As TM1Server
        Dim objTM1Process As TM1Process
        Dim objaParams(1) As TM1ProcessParameter


        Try

            objAdminSvr = New TM1AdminServer("MORGAN-ACER", "tm1adminserver")


            objServersCollection = objAdminSvr.Servers

            Debug.Print("The count of servers is " & CType(objServersCollection.Count, String))

            i_ObjCnt = objServersCollection.Count

            Debug.Print("List of servers")
            Debug.Print("-------------------------")

            For i_ObjIdx = 0 To i_ObjCnt - 1
                Debug.Print(objServersCollection.Item(i_ObjIdx).Name)
            Next

            Debug.Print(vbNewLine)

            objServerInfo = objServersCollection.Item("GO_New_Stores")

            objsDataServer = objServerInfo.Login("admin", "apple")



            Dim i As Integer, j As Integer, k As Integer
            Dim strServer As String, strCube As String, strView As String
            Dim node As TreeNode

            'Get servers
            For i = 1 To i_ObjCnt
                strServer = objServersCollection.Item(i - 1).Name
                node = Me.TreeView1.Nodes.Add(strServer, strServer, 0)
                node.SelectedImageIndex = 0

                'Get cubes
                For j = 1 To objsDataServer.Cubes.Count
                    strCube = objsDataServer.Cubes.Item(j - 1).Name

                    If strCube.Substring(0, 1) <> "}" Then
                        node = Me.TreeView1.Nodes(i - 1).Nodes.Add(strCube, strCube, 1)
                        node.SelectedImageIndex = 1

                        'Get cube public views
                        For k = 1 To objsDataServer.Cubes.Item(j - 1).PublicViews.Count
                            strView = objsDataServer.Cubes.Item(j - 1).PublicViews.Item(k - 1).Name
                            node = Me.TreeView1.Nodes(i - 1).Nodes(j - 1).Nodes.Add(strView, strView, 2)
                            node.SelectedImageIndex = 2
                        Next k

                        For k = 1 To objsDataServer.Cubes.Item(j - 1).PrivateViews.Count
                            strView = objsDataServer.Cubes.Item(j - 1).PrivateViews.Item(k - 1).Name
                            node = Me.TreeView1.Nodes(i - 1).Nodes(j - 1).Nodes.Add(strView, strView, 3)
                            node.SelectedImageIndex = 3
                        Next k

                    End If
                Next j
                'End If
            Next i

            Me.TreeView1.Nodes(0).Expand()




        Catch ex As Exception
            MessageBox.Show("Failed to complete the example. Error " _
             & ex.ToString, "TM1 Dot Net API Example", MessageBoxButtons.OK, _
             MessageBoxIcon.Error)
        End Try

        Try

            'the presence of a Dispose() method so if it's there, I'll use it.
            objTM1Process.Dispose()
            objsDataServer.Dispose()
            objServerInfo.Dispose()
            objServersCollection.Dispose()
            objAdminSvr.Dispose()

        Catch ex As Exception

        End Try
    End Sub



    Private Sub TreeView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseDown

        Dim NodeLevel As Integer
        Dim p As Point = New Point(e.X, e.Y)

        If e.Button = MouseButtons.Right Then

            'select node that is being right-clicked
            Me.TreeView1.SelectedNode = TreeView1.GetNodeAt(e.X, e.Y)

            'Get the nesting level of the selected node
            NodeLevel = Me.TreeView1.SelectedNode.Level()

            'only display context menu on lowest-level nodes
            If NodeLevel = 2 Then
                TreeView1.ContextMenuStrip = ContextMenuStrip1
            Else
                TreeView1.ContextMenuStrip = Nothing
            End If
        End If

        NodeLevel = -1

    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        ParaForm.Show()
    End Sub


    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Dispose()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutForm.Show()
    End Sub

    Private Sub InputToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InputToolStripMenuItem.Click
        Dim MF As New MDXForm(selectedServer)
        MF.Show()
    End Sub
End Class
