Option Explicit On
Imports Applix.TM1.API

Public Class Form1

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
     

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click


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

            Debug.Print("List of processes (" & _
             CType(objsDataServer.Processes.Count, String) & " in total.)")
            Debug.Print("-------------------------")

            i_ObjCnt = objsDataServer.Processes.Count

            For i_ObjIdx = 0 To i_ObjCnt - 1
                Debug.Print(objsDataServer.Processes.Item(i_ObjIdx).Name)
            Next



            objServerInfo.LogoutAll()

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
End Class
