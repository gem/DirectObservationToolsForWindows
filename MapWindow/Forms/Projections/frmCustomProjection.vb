Option Strict Off
'8/1/2006 - Paul Meems (pm) - Started Duth translation

'8/1/2006 pm changed
'Public Class frmProjSettings
Public Class frmCustomProjection
    Inherits System.Windows.Forms.Form

#Region "Declarations"
    Private pDB As clsProjectionDB
    '  FColl.FastCollection 'of custom projection specs
    'Private cProjs As FColl.FastCollection 'of standard projection specs
    'Private cElips As FColl.FastCollection 'of ellipsoids
    Private defaultsFlag As Boolean
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents txtSpheroid As System.Windows.Forms.TextBox
    Friend WithEvents cboSpheroid As System.Windows.Forms.ComboBox
    Friend WithEvents cboProjection As System.Windows.Forms.ComboBox
    Friend WithEvents lblD6 As System.Windows.Forms.Label
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents txtD6 As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblD5 As System.Windows.Forms.Label
    Friend WithEvents lblProjection As System.Windows.Forms.Label
    Friend WithEvents txtD5 As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblD4 As System.Windows.Forms.Label
    Friend WithEvents lblSpheroid As System.Windows.Forms.Label
    Friend WithEvents txtD4 As System.Windows.Forms.TextBox
    Friend WithEvents txtD1 As System.Windows.Forms.TextBox
    Friend WithEvents lblD3 As System.Windows.Forms.Label
    Friend WithEvents cboName As System.Windows.Forms.ComboBox
    Friend WithEvents txtD3 As System.Windows.Forms.TextBox
    Friend WithEvents lblD1 As System.Windows.Forms.Label
    Friend WithEvents lblD2 As System.Windows.Forms.Label
    Friend WithEvents txtD2 As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtProjSTring As System.Windows.Forms.TextBox
    Private pOK As Boolean
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        'May/12/2008 Jiri Kadlec - load icon from shared resources to reduce size of the program
        Me.Icon = My.Resources.MapWindow_new

        pDB = New clsProjectionDB
        SetControlsFromDB()
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomProjection))
        Me.btnOK = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtProjSTring = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblCategory = New System.Windows.Forms.Label
        Me.cboProjection = New System.Windows.Forms.ComboBox
        Me.lblD6 = New System.Windows.Forms.Label
        Me.cboCategory = New System.Windows.Forms.ComboBox
        Me.txtD6 = New System.Windows.Forms.TextBox
        Me.lblD5 = New System.Windows.Forms.Label
        Me.lblProjection = New System.Windows.Forms.Label
        Me.txtD5 = New System.Windows.Forms.TextBox
        Me.lblName = New System.Windows.Forms.Label
        Me.lblD4 = New System.Windows.Forms.Label
        Me.lblSpheroid = New System.Windows.Forms.Label
        Me.txtD4 = New System.Windows.Forms.TextBox
        Me.txtD1 = New System.Windows.Forms.TextBox
        Me.lblD3 = New System.Windows.Forms.Label
        Me.txtD3 = New System.Windows.Forms.TextBox
        Me.lblD1 = New System.Windows.Forms.Label
        Me.lblD2 = New System.Windows.Forms.Label
        Me.txtD2 = New System.Windows.Forms.TextBox
        Me.cboName = New System.Windows.Forms.ComboBox
        Me.cboSpheroid = New System.Windows.Forms.ComboBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtSpheroid = New System.Windows.Forms.TextBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.AccessibleDescription = Nothing
        Me.btnOK.AccessibleName = Nothing
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Nothing
        Me.btnOK.Font = Nothing
        Me.btnOK.Name = "btnOK"
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = Nothing
        Me.Label1.AccessibleName = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Font = Nothing
        Me.Label1.Name = "Label1"
        '
        'GroupBox1
        '
        Me.GroupBox1.AccessibleDescription = Nothing
        Me.GroupBox1.AccessibleName = Nothing
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.BackgroundImage = Nothing
        Me.GroupBox1.Controls.Add(Me.txtProjSTring)
        Me.GroupBox1.Font = Nothing
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'txtProjSTring
        '
        Me.txtProjSTring.AccessibleDescription = Nothing
        Me.txtProjSTring.AccessibleName = Nothing
        resources.ApplyResources(Me.txtProjSTring, "txtProjSTring")
        Me.txtProjSTring.BackgroundImage = Nothing
        Me.txtProjSTring.Font = Nothing
        Me.txtProjSTring.Name = "txtProjSTring"
        '
        'GroupBox2
        '
        Me.GroupBox2.AccessibleDescription = Nothing
        Me.GroupBox2.AccessibleName = Nothing
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.BackgroundImage = Nothing
        Me.GroupBox2.Controls.Add(Me.lblCategory)
        Me.GroupBox2.Controls.Add(Me.cboProjection)
        Me.GroupBox2.Controls.Add(Me.lblD6)
        Me.GroupBox2.Controls.Add(Me.cboCategory)
        Me.GroupBox2.Controls.Add(Me.txtD6)
        Me.GroupBox2.Controls.Add(Me.lblD5)
        Me.GroupBox2.Controls.Add(Me.lblProjection)
        Me.GroupBox2.Controls.Add(Me.txtD5)
        Me.GroupBox2.Controls.Add(Me.lblName)
        Me.GroupBox2.Controls.Add(Me.lblD4)
        Me.GroupBox2.Controls.Add(Me.lblSpheroid)
        Me.GroupBox2.Controls.Add(Me.txtD4)
        Me.GroupBox2.Controls.Add(Me.txtD1)
        Me.GroupBox2.Controls.Add(Me.lblD3)
        Me.GroupBox2.Controls.Add(Me.txtD3)
        Me.GroupBox2.Controls.Add(Me.lblD1)
        Me.GroupBox2.Controls.Add(Me.lblD2)
        Me.GroupBox2.Controls.Add(Me.txtD2)
        Me.GroupBox2.Controls.Add(Me.cboName)
        Me.GroupBox2.Controls.Add(Me.cboSpheroid)
        Me.GroupBox2.Controls.Add(Me.txtName)
        Me.GroupBox2.Controls.Add(Me.txtSpheroid)
        Me.GroupBox2.Font = Nothing
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'lblCategory
        '
        Me.lblCategory.AccessibleDescription = Nothing
        Me.lblCategory.AccessibleName = Nothing
        resources.ApplyResources(Me.lblCategory, "lblCategory")
        Me.lblCategory.Font = Nothing
        Me.lblCategory.Name = "lblCategory"
        '
        'cboProjection
        '
        Me.cboProjection.AccessibleDescription = Nothing
        Me.cboProjection.AccessibleName = Nothing
        resources.ApplyResources(Me.cboProjection, "cboProjection")
        Me.cboProjection.BackgroundImage = Nothing
        Me.cboProjection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProjection.Font = Nothing
        Me.cboProjection.Name = "cboProjection"
        Me.cboProjection.Sorted = True
        '
        'lblD6
        '
        Me.lblD6.AccessibleDescription = Nothing
        Me.lblD6.AccessibleName = Nothing
        resources.ApplyResources(Me.lblD6, "lblD6")
        Me.lblD6.Font = Nothing
        Me.lblD6.Name = "lblD6"
        '
        'cboCategory
        '
        Me.cboCategory.AccessibleDescription = Nothing
        Me.cboCategory.AccessibleName = Nothing
        resources.ApplyResources(Me.cboCategory, "cboCategory")
        Me.cboCategory.BackgroundImage = Nothing
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = Nothing
        Me.cboCategory.Name = "cboCategory"
        '
        'txtD6
        '
        Me.txtD6.AccessibleDescription = Nothing
        Me.txtD6.AccessibleName = Nothing
        resources.ApplyResources(Me.txtD6, "txtD6")
        Me.txtD6.BackgroundImage = Nothing
        Me.txtD6.Font = Nothing
        Me.txtD6.Name = "txtD6"
        '
        'lblD5
        '
        Me.lblD5.AccessibleDescription = Nothing
        Me.lblD5.AccessibleName = Nothing
        resources.ApplyResources(Me.lblD5, "lblD5")
        Me.lblD5.Font = Nothing
        Me.lblD5.Name = "lblD5"
        '
        'lblProjection
        '
        Me.lblProjection.AccessibleDescription = Nothing
        Me.lblProjection.AccessibleName = Nothing
        resources.ApplyResources(Me.lblProjection, "lblProjection")
        Me.lblProjection.Font = Nothing
        Me.lblProjection.Name = "lblProjection"
        '
        'txtD5
        '
        Me.txtD5.AccessibleDescription = Nothing
        Me.txtD5.AccessibleName = Nothing
        resources.ApplyResources(Me.txtD5, "txtD5")
        Me.txtD5.BackgroundImage = Nothing
        Me.txtD5.Font = Nothing
        Me.txtD5.Name = "txtD5"
        '
        'lblName
        '
        Me.lblName.AccessibleDescription = Nothing
        Me.lblName.AccessibleName = Nothing
        resources.ApplyResources(Me.lblName, "lblName")
        Me.lblName.Font = Nothing
        Me.lblName.Name = "lblName"
        '
        'lblD4
        '
        Me.lblD4.AccessibleDescription = Nothing
        Me.lblD4.AccessibleName = Nothing
        resources.ApplyResources(Me.lblD4, "lblD4")
        Me.lblD4.Font = Nothing
        Me.lblD4.Name = "lblD4"
        '
        'lblSpheroid
        '
        Me.lblSpheroid.AccessibleDescription = Nothing
        Me.lblSpheroid.AccessibleName = Nothing
        resources.ApplyResources(Me.lblSpheroid, "lblSpheroid")
        Me.lblSpheroid.Font = Nothing
        Me.lblSpheroid.Name = "lblSpheroid"
        '
        'txtD4
        '
        Me.txtD4.AccessibleDescription = Nothing
        Me.txtD4.AccessibleName = Nothing
        resources.ApplyResources(Me.txtD4, "txtD4")
        Me.txtD4.BackgroundImage = Nothing
        Me.txtD4.Font = Nothing
        Me.txtD4.Name = "txtD4"
        '
        'txtD1
        '
        Me.txtD1.AccessibleDescription = Nothing
        Me.txtD1.AccessibleName = Nothing
        resources.ApplyResources(Me.txtD1, "txtD1")
        Me.txtD1.BackgroundImage = Nothing
        Me.txtD1.Font = Nothing
        Me.txtD1.Name = "txtD1"
        '
        'lblD3
        '
        Me.lblD3.AccessibleDescription = Nothing
        Me.lblD3.AccessibleName = Nothing
        resources.ApplyResources(Me.lblD3, "lblD3")
        Me.lblD3.Font = Nothing
        Me.lblD3.Name = "lblD3"
        '
        'txtD3
        '
        Me.txtD3.AccessibleDescription = Nothing
        Me.txtD3.AccessibleName = Nothing
        resources.ApplyResources(Me.txtD3, "txtD3")
        Me.txtD3.BackgroundImage = Nothing
        Me.txtD3.Font = Nothing
        Me.txtD3.Name = "txtD3"
        '
        'lblD1
        '
        Me.lblD1.AccessibleDescription = Nothing
        Me.lblD1.AccessibleName = Nothing
        resources.ApplyResources(Me.lblD1, "lblD1")
        Me.lblD1.Font = Nothing
        Me.lblD1.Name = "lblD1"
        '
        'lblD2
        '
        Me.lblD2.AccessibleDescription = Nothing
        Me.lblD2.AccessibleName = Nothing
        resources.ApplyResources(Me.lblD2, "lblD2")
        Me.lblD2.Font = Nothing
        Me.lblD2.Name = "lblD2"
        '
        'txtD2
        '
        Me.txtD2.AccessibleDescription = Nothing
        Me.txtD2.AccessibleName = Nothing
        resources.ApplyResources(Me.txtD2, "txtD2")
        Me.txtD2.BackgroundImage = Nothing
        Me.txtD2.Font = Nothing
        Me.txtD2.Name = "txtD2"
        '
        'cboName
        '
        Me.cboName.AccessibleDescription = Nothing
        Me.cboName.AccessibleName = Nothing
        resources.ApplyResources(Me.cboName, "cboName")
        Me.cboName.BackgroundImage = Nothing
        Me.cboName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboName.Font = Nothing
        Me.cboName.Name = "cboName"
        Me.cboName.Sorted = True
        '
        'cboSpheroid
        '
        Me.cboSpheroid.AccessibleDescription = Nothing
        Me.cboSpheroid.AccessibleName = Nothing
        resources.ApplyResources(Me.cboSpheroid, "cboSpheroid")
        Me.cboSpheroid.BackgroundImage = Nothing
        Me.cboSpheroid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpheroid.Font = Nothing
        Me.cboSpheroid.Name = "cboSpheroid"
        Me.cboSpheroid.Sorted = True
        '
        'txtName
        '
        Me.txtName.AccessibleDescription = Nothing
        Me.txtName.AccessibleName = Nothing
        resources.ApplyResources(Me.txtName, "txtName")
        Me.txtName.BackgroundImage = Nothing
        Me.txtName.Font = Nothing
        Me.txtName.Name = "txtName"
        '
        'txtSpheroid
        '
        Me.txtSpheroid.AccessibleDescription = Nothing
        Me.txtSpheroid.AccessibleName = Nothing
        resources.ApplyResources(Me.txtSpheroid, "txtSpheroid")
        Me.txtSpheroid.BackgroundImage = Nothing
        Me.txtSpheroid.Font = Nothing
        Me.txtSpheroid.Name = "txtSpheroid"
        '
        'btnCancel
        '
        Me.btnCancel.AccessibleDescription = Nothing
        Me.btnCancel.AccessibleName = Nothing
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.BackgroundImage = Nothing
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = Nothing
        Me.btnCancel.Name = "btnCancel"
        '
        'frmCustomProjection
        '
        Me.AcceptButton = Me.btnOK
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnOK)
        Me.Font = Nothing
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = Nothing
        Me.MaximizeBox = False
        Me.Name = "frmCustomProjection"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

#Region "Projection Code"

    'Private pCurProjection As clsProjection

    Public Function AskUser() As String
        pOK = False
        Me.Show()
        Me.Visible = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.BringToFront()

        While Me.Visible
            System.Windows.Forms.Application.DoEvents()
            System.Threading.Thread.Sleep(100)
        End While

        If pOK Then
            AskUser = toString()
        Else
            AskUser = ""
        End If
    End Function

    'Examines the controls of the form and returns the projection string
    Public Overrides Function toString() As String
        'If txtProjSTring.Text.ToLower().StartsWith("+proj4=") And Not txtProjSTring.Text.ToLower() = "+proj4=" Then
        '30/4/09 G Wilson. Change condition so that a custom proj is actually used
        If txtProjSTring.Text.ToLower().StartsWith("+proj=") And Not txtProjSTring.Text.ToLower() = "+proj4=" Then
            Return txtProjSTring.Text
        End If

        Dim id As Integer
        Dim ProjObj As clsATCProjection = Nothing
        Dim ProjObjC As clsATCProjection = Nothing

        toString = ""
        ProjObj = pDB.StandardProjections.Item(cboCategory.Text & cboName.Text)
        toString += "+proj=" & Trim(ProjObj.ProjectionClass) & " "
        For Each ProjObjC In pDB.BaseProjections.Values
            If ProjObj.ProjectionClass = ProjObjC.ProjectionClass Or (ProjObj.ProjectionClass = "utm" And ProjObjC.ProjectionClass = "tmerc") Then
                If ProjObj.ProjectionClass = "utm" Then
                    toString += "+zone=" & ProjObj.Zone & " "
                End If
                Exit For
            End If
        Next

        ProjObj = pDB.Ellipsoids(Trim(txtSpheroid.Text))
        If Not ProjObj Is Nothing Then
            toString += "+ellps=" & Trim(ProjObj.ProjectionClass) & " "
        End If

        If Not ProjObjC Is Nothing Then
            Dim paramName As String
            For id = 1 To 6
                paramName = Trim(ProjObjC.d(id)) 'paramName = Trim(ProjObjC.d(1))
                If paramName.Length > 0 Then
                    toString += "+" & paramName & "=" & Trim(txtD(id).Text) & " "
                End If
            Next
        End If
    End Function

    Public Sub FromString(ByVal projString As String)
        Try
            If projString Is Nothing Then
                Return
            End If
            If projString = "" Then
                Return
            End If
            If Not projString.ToLower().StartsWith("+proj") Then
                Return
            End If

            Dim intermediate As String = projString.Replace("proj" + vbCrLf, "").Replace("end" + vbCrLf, "").Replace(" ", "|")
            Dim components() As String = intermediate.Split("|")

            Dim curProjection As New clsATCProjection

            Dim tempFieldValues As New ArrayList
            Dim tempFieldNames As New ArrayList
            Dim dIter As Integer = 1

            Dim i As Integer
            For i = 0 To components.Length - 1
                Dim valuepair() As String = components(i).Split("=")
                If (valuepair.Length = 2) Then
                    Select Case valuepair(0)
                        Case ""
                        Case "+proj"
                            curProjection.ProjectionClass = valuepair(1)
                        Case "+ellps"
                            curProjection.Ellipsoid = valuepair(1)
                        Case "+zone"
                            curProjection.Zone = valuepair(1)
                        Case Else
                            tempFieldValues.Add(valuepair(1).Trim())
                            tempFieldNames.Add(valuepair(0).Replace("+", "").Trim())
                            dIter += 1
                    End Select
                End If
            Next i

            Dim ProjObj As clsATCProjection = pDB.Ellipsoids(Trim(curProjection.Ellipsoid))
            Dim proj4Ellipsoid As String = Trim(ProjObj.ProjectionClass)

            'Find the proper projection
            Dim modelPrj As clsATCProjection = Nothing
            Dim prj As clsATCProjection
            For Each prj In pDB.StandardProjections.Values
                Dim pro2 As clsATCProjection = pDB.Ellipsoids(Trim(prj.Ellipsoid))
                Dim modelProj4Ellipsoid As String = Trim(pro2.ProjectionClass)
                Debug.WriteLine(modelProj4Ellipsoid)
                If (prj.ProjectionClass.Trim() = curProjection.ProjectionClass.Trim() And _
                     modelProj4Ellipsoid = proj4Ellipsoid) Then

                    Dim allMatched As Boolean = True
                    Dim fieldValue As String
                    For Each fieldValue In tempFieldValues
                        Dim j As Integer
                        Dim thisMatch As Boolean = False
                        For j = 1 To 7
                            If (fieldValue = prj.d(j)) Then
                                thisMatch = True
                                Exit For
                            End If
                        Next
                        allMatched = allMatched And thisMatch
                    Next

                    If allMatched Then
                        modelPrj = prj
                        Exit For
                    End If
                End If
            Next

            If modelPrj Is Nothing Then
                For Each prj In pDB.BaseProjections.Values
                    Dim pro2 As clsATCProjection = pDB.Ellipsoids(Trim(prj.Ellipsoid))
                    If Not pro2 Is Nothing Then
                        Dim modelProj4Ellipsoid As String = Trim(pro2.ProjectionClass)
                        If (prj.ProjectionClass.Trim() = curProjection.ProjectionClass.Trim() And _
                            modelProj4Ellipsoid = proj4Ellipsoid) Then
                            Dim allMatched As Boolean = True
                            Dim fieldValue As String
                            For Each fieldValue In tempFieldValues
                                Dim j As Integer
                                Dim thisMatch As Boolean = False
                                For j = 1 To 7
                                    If (fieldValue = prj.d(j)) Then
                                        thisMatch = True
                                        Exit For
                                    End If
                                Next
                                allMatched = allMatched And thisMatch
                            Next

                            If allMatched Then
                                modelPrj = prj
                                Exit For
                            End If
                        End If
                    End If
                Next
            End If

            If modelPrj Is Nothing Then
                For Each prj In pDB.Ellipsoids.Values
                    Dim pro2 As clsATCProjection = pDB.Ellipsoids(Trim(prj.Ellipsoid))
                    If Not pro2 Is Nothing Then
                        Dim modelProj4Ellipsoid As String = Trim(pro2.ProjectionClass)
                        If (prj.ProjectionClass.Trim() = curProjection.ProjectionClass.Trim() And _
                            modelProj4Ellipsoid = proj4Ellipsoid) Then
                            Dim allMatched As Boolean = True
                            Dim fieldValue As String
                            For Each fieldValue In tempFieldValues
                                Dim j As Integer
                                Dim thisMatch As Boolean = False
                                For j = 1 To 7
                                    If (fieldValue = prj.d(j)) Then
                                        thisMatch = True
                                        Exit For
                                    End If
                                Next
                                allMatched = allMatched And thisMatch
                            Next

                            If allMatched Then
                                modelPrj = prj
                                Exit For
                            End If
                        End If
                    End If
                Next
            End If

            If Not modelPrj Is Nothing Then
                'Load this projection data onto the screen
                cboCategory.Text = modelPrj.Category
                cboName.Text = modelPrj.Name
                cboSpheroid.Text = modelPrj.Ellipsoid

                Dim baseProjection As clsATCProjection = pDB.BaseProjections.Item(modelPrj.ProjectionClass)
                If Not baseProjection Is Nothing Then
                    For i = 1 To 6
                        If Len(baseProjection.Fieldnames.d(i)) > 0 Then
                            lblD(i).Text = baseProjection.Fieldnames.d(i)
                            lblD(i).Visible = True
                            txtD(i).Visible = True
                        End If
                    Next i
                End If
                'If modelPrj.Fieldnames Is Nothing Then
                '    For i = 0 To tempFieldNames.Count - 1
                '        lblD(i + 1).Text = tempFieldNames(i)
                '    Next
                'Else
                '    For i = 1 To 7
                '        If Not modelPrj.Fieldnames.d(i) Is Nothing Then
                '            lblD(i).Text = modelPrj.Fieldnames.d(i)
                '        Else
                '            lblD(i).Text = tempFieldNames(i - 1)
                '        End If
                '    Next
                'End If
                For i = 0 To tempFieldValues.Count - 1
                    txtD(i + 1).Text = tempFieldValues(i)
                Next

            End If
        Catch
        End Try
    End Sub

    Private Sub cboSpheroid_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSpheroid.SelectedIndexChanged
        txtSpheroid.Text = cboSpheroid.Text
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        Dim curProjection As clsATCProjection

        cboName.Items.Clear()
        If Not pDB Is Nothing Then
            For Each curProjection In pDB.StandardProjections.Values
                If curProjection.Category = cboCategory.Text Then
                    If Not cboName.Items.Contains(curProjection.Name) Then
                        cboName.Items.Add(curProjection.Name)
                    End If
                End If
            Next
            If cboCategory.SelectedIndex = 2 Then
                cboName.SelectedIndex = 1
            Else
                cboName.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub SetControlsFromDB()
        Dim prj As clsATCProjection

        If pDB.StandardProjections.Count > 0 Then
            For Each prj In pDB.StandardProjections.Values
                If Not cboCategory.Items.Contains(prj.Category) Then
                    cboCategory.Items.Add(prj.Category)
                End If
            Next

            For Each prj In pDB.BaseProjections.Values
                cboProjection.Items.Add(prj.Name)
            Next
            cboProjection.Visible = False

            For Each prj In pDB.Ellipsoids.Values
                cboSpheroid.Items.Add(prj.Ellipsoid)
            Next


            cboCategory.SelectedIndex = 0

            defaultsFlag = True 'used to specify if visible projection defaults should be updated
            btnOK.Enabled = True
        End If
    End Sub

    Private Function lblD(ByVal index As Integer) As Windows.Forms.Label
        Select Case index
            Case 1 : Return lblD1
            Case 2 : Return lblD2
            Case 3 : Return lblD3
            Case 4 : Return lblD4
            Case 5 : Return lblD5
            Case 6 : Return lblD6
            Case Else : Return Nothing
        End Select
    End Function

    Private Function txtD(ByVal index As Integer) As Windows.Forms.TextBox
        Select Case index
            Case 1 : Return txtD1
            Case 2 : Return txtD2
            Case 3 : Return txtD3
            Case 4 : Return txtD4
            Case 5 : Return txtD5
            Case 6 : Return txtD6
            Case Else : Return Nothing
        End Select
    End Function

    Private Sub cboProjection_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProjection.SelectedIndexChanged
        Dim id As Long
        Dim CurProjection As clsATCProjection

        If defaultsFlag Then
            CurProjection = pDB.BaseProjections(cboProjection.Text)
            If InStr(1, CurProjection.Ellipsoid, "=") > 0 Then
                'case where projection determines ellipsoid
                cboSpheroid.Visible = False
                txtSpheroid.Text = Mid(CurProjection.Ellipsoid, InStr(1, CurProjection.Ellipsoid, "=") + 1)
                txtSpheroid.Visible = True
            Else
                'user can select ellipsoid
                cboSpheroid.Text = CurProjection.Ellipsoid
                cboSpheroid.Visible = True
                txtSpheroid.Visible = False
            End If
            For id = 1 To 6
                If Len(CurProjection.d(id)) > 0 Then
                    txtD(id).Text = CurProjection.Defaults.d(id)
                    lblD(id).Text = CurProjection.Fieldnames.d(id) & ":"
                    txtD(id).Visible = True
                    lblD(id).Visible = True
                    txtD(id).Enabled = True
                Else
                    txtD(id).Text = ""
                    lblD(id).Visible = False
                    txtD(id).Visible = False
                End If
            Next
        End If
    End Sub

    Private Sub cboName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboName.SelectedIndexChanged
        Dim id As Long
        Dim lClass As String
        Dim curProjection As clsATCProjection
        Dim baseProjection As clsATCProjection


        cboSpheroid.Items.Clear()
        If Not pDB Is Nothing Then
            For Each curProjection In pDB.StandardProjections.Values
                If curProjection.Category = cboCategory.Text And curProjection.Name = cboName.Text Then
                    If Not cboSpheroid.Items.Contains(curProjection.Ellipsoid) Then
                        cboSpheroid.Items.Add(curProjection.Ellipsoid)
                    End If
                End If
            Next
        End If


        curProjection = pDB.StandardProjections(cboCategory.Text & cboName.Text)
        lClass = curProjection.ProjectionClass
        If lClass = "utm" Then lClass = "tmerc"

        cboSpheroid.Text = curProjection.Ellipsoid

        For id = 1 To 6
            If Len(curProjection.d(id)) > 0 Then
                txtD(id).Text = curProjection.d(id)
            Else
                txtD(id).Text = 0
            End If
        Next

        For id = 1 To 6
            lblD(id).Visible = False
            txtD(id).Visible = False
        Next

        If lClass = "dd" Then
            'leave them all invisible
        Else
            baseProjection = pDB.BaseProjections.Item(curProjection.ProjectionClass)
            If Not baseProjection Is Nothing Then
                For id = 1 To 6
                    If Len(baseProjection.Fieldnames.d(id)) > 0 Then
                        lblD(id).Text = baseProjection.Fieldnames.d(id)
                        lblD(id).Visible = True
                        txtD(id).Visible = True
                        txtD(id).Enabled = True
                    End If
                Next id
            End If
        End If
    End Sub

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub frmProjSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FromString(ProjInfo.ProjectProjection)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
