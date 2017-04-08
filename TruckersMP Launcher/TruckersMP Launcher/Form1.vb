Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Security.Cryptography
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Web.Script.Serialization
Imports Microsoft.Win32

Public Class Form1

    Private Const BUF_SIZE As Integer = 65536
    ''' <summary>
    ''' Returns the file integrity checksum hash, otherwise an empty string.
    ''' </summary>
    Public Shared Function IntegrityCheck(ByVal filePath As String) As String
        Dim dataBuffer(BUF_SIZE - 1) As Byte
        Dim dataBufferDummy(BUF_SIZE - 1) As Byte
        Dim dataBytesRead As Integer = 0
        Dim hashResult As String = String.Empty
        Dim hashAlg As HashAlgorithm = Nothing
        Dim fs As FileStream = Nothing

        Try
            hashAlg = New MD5CryptoServiceProvider
            fs = New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, BUF_SIZE)
            Do
                dataBytesRead = fs.Read(dataBuffer, 0, BUF_SIZE)
                hashAlg.TransformBlock(dataBuffer, 0, dataBytesRead, dataBufferDummy, 0)
            Loop Until dataBytesRead = 0
            hashAlg.TransformFinalBlock(dataBuffer, 0, 0)
            hashResult = BitConverter.ToString(hashAlg.Hash).Replace("-", "").ToLower
        Catch ex As IOException
            CrashHandler.HandleCrash(ex)
            MsgBox("This crash is critical, this program will now close.", MsgBoxStyle.Critical, "Critical Error!")
            Application.Exit()
        Catch ex As UnauthorizedAccessException
            CrashHandler.HandleCrash(ex)
            MsgBox("This crash is critical, this program will now close.", MsgBoxStyle.Critical, "Critical Error!")
            Application.Exit()
        Finally
            If Not fs Is Nothing Then
                fs.Close()
                fs.Dispose()
                fs = Nothing
            End If
            If Not hashAlg Is Nothing Then
                hashAlg.Clear()
                hashAlg = Nothing
            End If
        End Try
        Return hashResult
    End Function

    Public TickTock As Integer = 30
    Public Excepted As Exception
    Public NewsShown As Boolean

    Public Class sresponse
        Public Property id As Integer
        Public Property game As String
        'Public Property ip As String
        'Public Property port As Integer
        Public Property name As String
        Public Property shortname As String
        Public Property online As Boolean
        Public Property players As Integer
        Public Property queue As Integer
        Public Property maxplayers As Integer
        Public Property speedlimiter As Integer
    End Class

    ' Public Class Permissions
    'Public Property isGameAdmin As Boolean
    'Public Property showDetailedOnWebMaps As Boolean
    'End Class

    Public Class presponse
        Public Property pid As Integer
        Public Property pname As String
        Public Property avatar As String
        Public Property joinDate As String
        Public Property steamID64 As Long
        Public Property groupName As String
        'Public Property permissions As Permissions
    End Class

    Public Class bresponse
        Public Property expiration As String
        Public Property timeAdded As String
        Public Property active As Boolean
        Public Property reason As String
        Public Property adminName As String
        Public Property adminID As Integer
    End Class

    Public Class rresponse
        'Public Property error As Boolean
        Public Property rules As String
        'Public Property revision As Integer
    End Class

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim checkversion As WebClient = New WebClient()
        checkversion.CachePolicy = New System.Net.Cache.RequestCachePolicy(Cache.RequestCacheLevel.BypassCache)
        Dim versionresponse As String = checkversion.DownloadString("https://raw.githubusercontent.com/NarodGaming/narodtruckersmplauncher/master/Updates/currentver.txt")

        MsgBox(versionresponse)

        If Not versionresponse = Application.ProductVersion Then
            Dim wouldliketoupdate As MsgBoxResult = MsgBox("A new version is available, would you like to download it?", MsgBoxStyle.YesNo, "An update is available!")
            If wouldliketoupdate = MsgBoxResult.Yes Then
                Process.Start("https://github.com/NarodGaming/narodtruckersmplauncher/releases")
            Else
                MsgBox("OK. Be warned though - new versions will include bugfixes, extra features and much more!", MsgBoxStyle.Information, "Information")
            End If
        End If

        lbl_launcher_ver.Text = "Launcher Version: " + Application.ProductVersion

        Try
            Dim version As String = "https://api.truckersmp.com/v2/version"
            Dim vclient As WebClient = New WebClient()
            Dim vreader As StreamReader = New StreamReader(vclient.OpenRead(version))
            Dim vjson As String = vreader.ReadToEnd()


            Dim rules As Object = New JavaScriptSerializer().Deserialize(Of Object)(vjson)
            Dim vtruckersmp = rules("name")
            Dim vntruckersmp = rules("numeric")
            Dim struckersmp = rules("stage")
            Dim ets2checkdict = rules("ets2mp_checksum")
            Dim ets2dllhash = rules("ets2mp_checksum")("dll")
            Dim ets2adbhash = rules("ets2mp_checksum")("adb")
            Dim atscheckdict = rules("atsmp_checksum")
            Dim atsdllhash = rules("atsmp_checksum")("dll")
            Dim atsadbhash = rules("atsmp_checksum")("adb")
            Dim timereleased = rules("time")
            Dim ets2suppver = rules("supported_game_version")
            Dim atssuppver = rules("supported_ats_game_version")

            lbl_truckersmp_ver.Text = "TruckersMP Latest Version: " + vtruckersmp
            lbl_supp_ets2.Text = "Supported ETS2 Version: " + ets2suppver
            lbl_supp_ats.Text = "Supported ATS Version: " + atssuppver
            lbl_date_release.Text = "Latest Released on: " + timereleased

            Dim atsupdateneeded As Boolean = False
            Dim ets2updateneeded As Boolean = False

            If Not File.Exists("C:\ProgramData\TruckersMP\data\ets2\data1.adb") Then
                ets2updateneeded = True
            ElseIf Not File.Exists("C:\ProgramData\TruckersMP\data\ats\data1.adb") Then
                atsupdateneeded = True
            End If


            'MsgBox(IntegrityCheck("C:\ProgramData\TruckersMP\data\ets2\data1.adb") + vbCrLf + ets2adbhash)
            'MsgBox(IntegrityCheck("C:\ProgramData\TruckersMP\data\ats\data1.adb") + vbCrLf + atsadbhash)

            If ets2updateneeded = False Then
                If Not IntegrityCheck("C:\ProgramData\TruckersMP\data\ets2\data1.adb") = ets2adbhash Then
                    ets2updateneeded = True
                End If
            ElseIf atsupdateneeded = False Then
                If Not IntegrityCheck("C:\ProgramData\TruckersMP\data\ats\data1.adb") = atsadbhash Then
                    atsupdateneeded = True
                End If
            End If

            If ets2updateneeded = True Then
                btn_ets2mp.Text = "Update Euro Truck Simulator 2 MP"
                lbl_current_truckersmp_ver.Text = "Current TruckersMP Version: Out-Of-Date"
            ElseIf atsupdateneeded = True Then
                btn_atsmp.Text = "Update American Truck Simulator MP"
                lbl_current_truckersmp_ver.Text = "Current TruckersMP Version: Out-Of-Date"
            ElseIf ets2updateneeded = False And atsupdateneeded = False Then
                lbl_current_truckersmp_ver.Text = "Current TruckersMP Version: Up-To-Date"
            End If

        Catch ex As Exception
            CrashHandler.HandleCrash(ex)
            Me.Dispose()
        End Try




        Dim SuccessOnLoad As Boolean = ServersUpdate()
        If SuccessOnLoad = False Then
            CrashHandler.HandleCrash(Excepted)
            Me.Dispose()
        Else
            refresh_Timer.Start()
        End If
    End Sub

    Private Function ServersUpdate()
        Try

            Dim serversadd As String = "https://api.truckersmp.com/v2/servers"
            Dim sclient As WebClient = New WebClient()
            Dim sreader As StreamReader = New StreamReader(sclient.OpenRead(serversadd))
            Dim sjson As String = sreader.ReadToEnd()

            Dim sobj = JObject.Parse(sjson)
            Dim sdata As List(Of JToken) = sobj.Children().ToList
            Dim soutput As String = ""
            Dim sinteger As Integer = 1

            For Each sitem As JProperty In sdata
                sitem.CreateReader()
                Select Case sitem.Name
                    Case "response"
                        soutput += "Servers:" + vbCrLf
                        For Each server As JObject In sitem.Values
                            Dim id As String = server("id")
                            Dim game As String = server("game")
                            Dim sname As String = server("name")
                            Dim sshortname As String = server("shortname")
                            Dim sonline As String = server("online")
                            Dim players As String = server("players")
                            Dim queue As String = server("queue")
                            Dim maxplayers As String = server("maxplayers")
                            Dim speedlimiter As String = server("speedlimiter")
                            soutput += id + vbCrLf + game + vbCrLf + sname + vbCrLf + sshortname + vbCrLf + sonline + vbCrLf + players + vbCrLf + queue + vbCrLf + maxplayers + vbCrLf + speedlimiter + vbCrLf + vbCrLf

                            Select Case sinteger
                                Case "1"
                                    s1_Name.Text = sname
                                    p1_Name.Text = "Players: " + players + " / " + maxplayers
                                    g1_Name.Text = game
                                    If queue = "0" Then
                                        q1_Name.Visible = False
                                        q1_Name.Text = queue
                                    Else
                                        q1_Name.Visible = True
                                        q1_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st1_Name.Text = "Online: Yes"
                                    Else
                                        st1_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp1_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp1_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh1_Name.Text = sshortname
                                    Dim spbar As Decimal = players / maxplayers * 100
                                    t1_Total.Value = spbar
                                    If t1_Total.Value = 100 And q1_Name.Visible = False Then
                                        q1_Name.Visible = True
                                        q1_Name.Text = "Full, but no queue"
                                    End If
                                    sinteger += 1
                                Case "2"
                                    s2_Name.Text = sname
                                    p2_Name.Text = "Players: " + players + " / " + maxplayers
                                    g2_Name.Text = game
                                    If queue = "0" Then
                                        q2_Name.Visible = False
                                        q2_Name.Text = queue
                                    Else
                                        q2_Name.Visible = True
                                        q2_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st2_Name.Text = "Online: Yes"
                                    Else
                                        st2_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp2_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp2_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh2_Name.Text = sshortname
                                    Dim spbar As Decimal = players / maxplayers * 100
                                    t2_Total.Value = spbar
                                    If t2_Total.Value = 100 And q2_Name.Visible = False Then
                                        q2_Name.Visible = True
                                        q2_Name.Text = "Full, but no queue"
                                    End If
                                    sinteger += 1
                                Case "3"
                                    s3_Name.Text = sname
                                    p3_Name.Text = "Players: " + players + " / " + maxplayers
                                    g3_Name.Text = game
                                    If queue = "0" Then
                                        q3_Name.Visible = False
                                        q3_Name.Text = queue
                                    Else
                                        q3_Name.Visible = True
                                        q3_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st3_Name.Text = "Online: Yes"
                                    Else
                                        st3_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp3_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp3_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh3_Name.Text = sshortname
                                    Dim spbar As Decimal = players / maxplayers * 100
                                    t3_Total.Value = spbar
                                    If t3_Total.Value = 100 And q3_Name.Visible = False Then
                                        q3_Name.Visible = True
                                        q3_Name.Text = "Full, but no queue"
                                    End If
                                    sinteger += 1
                                Case "4"
                                    s4_Name.Text = sname
                                    p4_Name.Text = "Players: " + players + " / " + maxplayers
                                    g4_Name.Text = game
                                    If queue = "0" Then
                                        q4_Name.Visible = False
                                        q4_Name.Text = queue
                                    Else
                                        q4_Name.Visible = True
                                        q4_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st4_Name.Text = "Online: Yes"
                                    Else
                                        st4_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp4_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp4_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh4_Name.Text = sshortname
                                    Dim spbar As Decimal = players / maxplayers * 100
                                    t4_Total.Value = spbar
                                    If t4_Total.Value = 100 And q4_Name.Visible = False Then
                                        q4_Name.Visible = True
                                        q4_Name.Text = "Full, but no queue"
                                    End If
                                    sinteger += 1
                                Case "5"
                                    s5_Name.Text = sname
                                    p5_Name.Text = "Players: " + players + " / " + maxplayers
                                    g5_Name.Text = game
                                    If queue = "0" Then
                                        q5_Name.Visible = False
                                        q5_Name.Text = queue
                                    Else
                                        q5_Name.Visible = True
                                        q5_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st5_Name.Text = "Online: Yes"
                                    Else
                                        st5_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp5_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp5_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh5_Name.Text = sshortname
                                    Dim spbar As Decimal = players / maxplayers * 100
                                    t5_Total.Value = spbar
                                    If t5_Total.Value = 100 And q5_Name.Visible = False Then
                                        q5_Name.Visible = True
                                        q5_Name.Text = "Full, but no queue"
                                    End If
                                    sinteger += 1
                                Case "6"
                                    s6_Name.Text = sname
                                    p6_Name.Text = "Players: " + players + " / " + maxplayers
                                    g6_Name.Text = game
                                    If queue = "0" Then
                                        q6_Name.Visible = False
                                        q6_Name.Text = queue
                                    Else
                                        q6_Name.Visible = True
                                        q6_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st6_Name.Text = "Online: Yes"
                                    Else
                                        st6_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp6_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp6_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh6_Name.Text = sshortname
                                    Dim spbar As Decimal = players / maxplayers * 100
                                    t6_Total.Value = spbar
                                    If t6_Total.Value = 100 And q6_Name.Visible = False Then
                                        q6_Name.Visible = True
                                        q6_Name.Text = "Full, but no queue"
                                    End If
                                    sinteger += 1
                                Case "7"
                                    s7_Name.Text = sname
                                    p7_Name.Text = "Players: " + players + " / " + maxplayers
                                    g7_Name.Text = game
                                    If queue = "0" Then
                                        q7_Name.Visible = False
                                        q7_Name.Text = queue
                                    Else
                                        q7_Name.Visible = True
                                        q7_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st7_Name.Text = "Online: Yes"
                                    Else
                                        st7_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp7_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp7_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh7_Name.Text = sshortname
                                    Dim spbar As Decimal = players / maxplayers * 100
                                    t7_Total.Value = spbar
                                    If t7_Total.Value = 100 And q7_Name.Visible = False Then
                                        q7_Name.Visible = True
                                        q7_Name.Text = "Full, but no queue"
                                    End If
                                    sinteger += 1
                                Case "8"
                                    s8_Name.Text = sname
                                    p8_Name.Text = "Players: " + players + " / " + maxplayers
                                    g8_Name.Text = game
                                    If queue = "0" Then
                                        q8_Name.Visible = False
                                        q8_Name.Text = queue
                                    Else
                                        q8_Name.Visible = True
                                        q8_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st8_Name.Text = "Online: Yes"
                                    Else
                                        st8_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp8_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp8_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh8_Name.Text = sshortname
                                    Dim spbar As Decimal = players / maxplayers * 100
                                    t8_Total.Value = spbar
                                    If t8_Total.Value = 100 And q8_Name.Visible = False Then
                                        q8_Name.Visible = True
                                        q8_Name.Text = "Full, but no queue"
                                    End If
                                    sinteger += 1


                            End Select
                        Next

                End Select
            Next
        Catch ex As Exception
            Excepted = ex
            Return False
        End Try
        Return True
    End Function

    Private Sub refresh_Timer_Tick(sender As Object, e As EventArgs) Handles refresh_Timer.Tick
        If TickTock = 0 Then
            refresh_Timer.Stop()
            Dim Success As Boolean = ServersUpdate()
            If Success = False Then
                CrashHandler.HandleCrash(Excepted)
                Me.Dispose()
            Else
                refresh_Timer.Start()
                TickTock = 30
                refresh_Time.Text = "Refreshing in 30 seconds"
            End If
        Else
            TickTock -= 1
            refresh_Time.Text = "Refreshing in " + TickTock.ToString + " seconds"
        End If
    End Sub

    Private Sub refresh_Now_Click(sender As Object, e As EventArgs) Handles refresh_Now.Click
        refresh_Timer.Stop()
        Dim Success As Boolean = ServersUpdate()
        If Success = False Then
            CrashHandler.HandleCrash(Excepted)
            Me.Dispose()
        Else
            refresh_Timer.Start()
            TickTock = 30
            refresh_Time.Text = "Refreshing in 30 seconds"
        End If
    End Sub

    Private Sub btn_Quit_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    Private Sub btn_Server_Click(sender As Object, e As EventArgs) Handles btn_Server.Click
        btn_Server.Enabled = False
        btn_News.Enabled = True
        btn_Settings.Enabled = True
        btn_Tools.Enabled = True
        btn_Play.Enabled = True
        pnl_news.Visible = False
        pnl_server.Visible = True
        pnl_tools.Visible = False
        pnl_play.Visible = False
        pnl_settings.Visible = False
    End Sub

    Private Sub btn_News_Click(sender As Object, e As EventArgs) Handles btn_News.Click
        news_Browser.Navigate("http://truckersmp.com/blog")
        NewsShown = True

        btn_Server.Enabled = True
        btn_News.Enabled = False
        btn_Settings.Enabled = True
        btn_Tools.Enabled = True
        btn_Play.Enabled = True
        pnl_news.Visible = True
        pnl_server.Visible = False
        pnl_tools.Visible = False
        pnl_play.Visible = False
        pnl_settings.Visible = False
    End Sub

    Private Sub btn_Tools_Click(sender As Object, e As EventArgs) Handles btn_Tools.Click
        btn_Server.Enabled = True
        btn_News.Enabled = True
        btn_Settings.Enabled = True
        btn_Tools.Enabled = False
        btn_Play.Enabled = True
        pnl_news.Visible = False
        pnl_server.Visible = False
        pnl_tools.Visible = True
        pnl_play.Visible = False
        pnl_settings.Visible = False
    End Sub

    Private Sub btn_Settings_Click(sender As Object, e As EventArgs) Handles btn_Settings.Click
        btn_Server.Enabled = True
        btn_News.Enabled = True
        btn_Settings.Enabled = False
        btn_Tools.Enabled = True
        btn_Play.Enabled = True
        pnl_news.Visible = False
        pnl_server.Visible = False
        pnl_tools.Visible = False
        pnl_play.Visible = False
        pnl_settings.Visible = True
    End Sub

    Private Sub player_info_search_Click(sender As Object, e As EventArgs) Handles player_info_search.Click
        Try
            Dim playerinfo As String = "https://api.truckersmp.com/v2/player/" + player_name_text.Text
            Dim pclient As WebClient = New WebClient()
            Dim preader As StreamReader = New StreamReader(pclient.OpenRead(playerinfo))
            Dim pjson As String = preader.ReadToEnd()


            Dim rules As Object = New JavaScriptSerializer().Deserialize(Of Object)(pjson)
            Dim errorname = rules("error")
            Dim response = rules("response")
            Dim id = rules("response")("id")
            Dim pname = rules("response")("name")
            Dim avatar = rules("response")("avatar")
            Dim joindate = rules("response")("joinDate")
            Dim steamID64 = rules("response")("steamID64")
            Dim groupname = rules("response")("groupName")
            Dim permissions = rules("response")("permissions")
            Dim gameadmin = rules("response")("permissions")("isGameAdmin")

            player_info_id.Text = "TruckersMP ID: " + id.ToString
            player_info_name.Text = pname.ToString
            player_info_img.Navigate(avatar)
            player_info_date.Text = "Joined TruckersMP on: " + joindate.ToString
            player_info_steamid.Text = "SteamID64: " + steamID64.ToString
            player_info_group.Text = "This user is a: " + groupname.ToString

            If gameadmin = True Then
                player_info_admin.Text = "Warning! This player is an administrator!"
            Else
                player_info_admin.Text = "This player is not an administrator."
            End If


        Catch ex As Exception
            CrashHandler.HandleCrash(ex)
            Me.Dispose()
        End Try

    End Sub

    Private Sub player_info_ban_Click(sender As Object, e As EventArgs) Handles player_info_ban.Click
        Try
            player_ban_table.Items.Clear()


            Dim baninfo As String = "https://api.truckersmp.com/v2/bans/" + player_name_text.Text
            Dim bclient As WebClient = New WebClient()
            Dim breader As StreamReader = New StreamReader(bclient.OpenRead(baninfo))
            Dim bjson As String = breader.ReadToEnd()

            Dim bobj As JObject = JObject.Parse(bjson)
            Dim bdata As List(Of JToken) = bobj.Children().ToList
            Dim boutput As String = ""

            For Each bitem As JProperty In bdata
                bitem.CreateReader()
                Select Case bitem.Name
                    Case "response"
                        boutput += "Player information:" + vbCrLf
                        For Each info As JObject In bitem.Values
                            Dim expiration As String = info("expiration")
                            Dim timeAdded As String = info("timeAdded")
                            Dim active As String = info("active")
                            Dim reason As String = info("reason")
                            Dim adminName As String = info("adminName")
                            boutput += expiration + vbCrLf + timeAdded + vbCrLf + active + vbCrLf + reason + vbCrLf + adminName + vbCrLf + vbCrLf

                            Dim AddMe As New ListViewItem
                            AddMe.Text = (timeAdded)
                            AddMe.SubItems.Add(expiration)
                            If active = "True" Then
                                AddMe.SubItems.Add("No")
                            Else
                                AddMe.SubItems.Add("Yes")
                            End If
                            AddMe.SubItems.Add(reason)
                            AddMe.SubItems.Add(adminName)
                            player_ban_table.Items.Add(AddMe)
                        Next
                End Select
            Next


        Catch ex As Exception
            CrashHandler.HandleCrash(ex)
            Me.Dispose()
        End Try
    End Sub

    Private Sub btn_Play_Click(sender As Object, e As EventArgs) Handles btn_Play.Click
        If My.Settings.RulesShown = False Then
            Try
                Dim rulesweb As String = "https://api.truckersmp.com/v2/rules"
                Dim rclient As WebClient = New WebClient()
                Dim rreader As StreamReader = New StreamReader(rclient.OpenRead(rulesweb))
                Dim rjson As String = rreader.ReadToEnd()

                Dim rules As Object = New JavaScriptSerializer().Deserialize(Of Object)(rjson)
                Dim errorname = rules("error")
                Dim rulesstring = rules("rules")
                Dim revision = rules("revision")

                Rules_Popup.RecieveRules(rulesstring)
            Catch ex As Exception
                CrashHandler.HandleCrash(ex)
                Me.Dispose()
            End Try

        Else
            RulesDone()
        End If
    End Sub

    Public Function RulesDone()
        btn_Server.Enabled = True
        btn_News.Enabled = True
        btn_Settings.Enabled = True
        btn_Tools.Enabled = True
        btn_Play.Enabled = False
        pnl_news.Visible = False
        pnl_server.Visible = False
        pnl_tools.Visible = False
        pnl_play.Visible = True
        pnl_settings.Visible = False
    End Function

    Private Sub btn_atsmp_Click(sender As Object, e As EventArgs) Handles btn_atsmp.Click
        Try
            Dim readValue = My.Computer.Registry.GetValue(
        "HKEY_LOCAL_MACHINE\SOFTWARE\TruckersMP", "InstallDir", Nothing)

            Process.Start(readValue + "\Launcher.exe")
            Application.Exit()
        Catch ex As Exception
            MsgBox("Could not open TruckersMP official launcher!" + vbCrLf + "Sending you to the download page...", MsgBoxStyle.Exclamation, "Error")
            Process.Start("https://truckersmp.com/download")
        End Try
    End Sub

    Private Sub btn_atssp_Click(sender As Object, e As EventArgs) Handles btn_atssp.Click
        Try
            Dim readValue = My.Computer.Registry.GetValue(
    "HKEY_LOCAL_MACHINE\SOFTWARE\TruckersMP", "InstallLocationATS", Nothing)

            Process.Start(readValue + "\bin\win_x64\amtrucks.exe")
            Application.Exit()
        Catch ex As Exception
            MsgBox("Could not open American Truck Simulator, is it installed?", MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    Private Sub btn_ets2mp_Click(sender As Object, e As EventArgs) Handles btn_ets2mp.Click
        Try
            Dim readValue = My.Computer.Registry.GetValue(
        "HKEY_LOCAL_MACHINE\SOFTWARE\TruckersMP", "InstallDir", Nothing)

            Process.Start(readValue + "\Launcher.exe")
            Application.Exit()
        Catch ex As Exception
            MsgBox("Could not open TruckersMP official launcher!" + vbCrLf + "Sending you to the download page...", MsgBoxStyle.Exclamation, "Error")
            Process.Start("https://truckersmp.com/download")
        End Try
    End Sub

    Private Sub btn_ets2sp_Click(sender As Object, e As EventArgs) Handles btn_ets2sp.Click
        Try
            Dim readValue = My.Computer.Registry.GetValue(
        "HKEY_LOCAL_MACHINE\SOFTWARE\TruckersMP", "InstallLocationETS2", Nothing)

            Process.Start(readValue + "\bin\win_x64\eurotrucks2.exe")
            Application.Exit()
        Catch ex As Exception
            MsgBox("Could not open Euro Truck Simulator 2, is it installed?", MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub



    'Private Sub news_Browser_Navigating(sender As Object, e As WebBrowserNavigatingEventArgs) Handles news_Browser.Navigating
    'If Not e.Url.ToString.Contains("truckersmp.com") And NewsShown = True Then
    'MsgBox("Whoops! You must stay on the TruckersMP site!", MsgBoxStyle.Exclamation, "Warning!")
    'news_Browser.Navigate("https://truckersmp.com/blog")
    'End If
    'End Sub
End Class
