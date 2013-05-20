Imports SharpKml.Base
Imports SharpKml.Dom
Imports SharpKml.Engine
Imports System
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.SQLite
Imports System.Net

Public Class frmGEM2KML

    Private pStrDB As String
    Private counter As Long = 0 ' to ensure and ids as unique

    Private Sub Gem2KML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = frmMain.Icon
        If Not gemdb Is Nothing Then
            Me.GEMDatabase.Text = gemdb.DatabasePath
        End If
        Me.IncludeImages.Checked = True
    End Sub

    Sub MakeKMZ(ByVal strGEMDatabase As String, ByVal kmzFilePath As String)
        pStrDB = strGEMDatabase
        Dim strImagePath As String = "FILENAME"
        '
        ' Create KML document
        '
        Dim pDocument As Document = New Document
        pDocument.Name = "GEM"
        Dim pDescription As SharpKml.Dom.Description = New SharpKml.Dom.Description
        pDescription.Text="GEM layers"
        pDocument.Description = pDescription
        ''
        '' Create kml for photographs
        ''
        Dim strSQL As String = "SELECT * FROM MEDIA_DETAIL_DECODE WHERE (MEDIA_TYPE='Photograph' OR MEDIA_TYPE='Sketch')"
        Dim pPhotosDataTable As DataTable = GetDataTableFromDatabase(strGEMDatabase, strSQL, "Photographs")
        'Dim pFolder As Folder = DataTablePlusImage2KML(pDocument, pPhotosDataTable, "Photographs", "FILENAME", "X", "Y")
        'pDocument.AddFeature(pFolder)
        '
        ' Create KML for Buildings
        '
        strSQL = "SELECT * FROM BUILDING_DECODE"
        Dim pDataTable As DataTable = GetDataTableFromDatabase(strGEMDatabase, strSQL, "Buildings")
        Dim pFolder2 As Folder = DataTable2KML(pDocument, pDataTable, "Buildings", "X", "Y")
        pDocument.AddFeature(pFolder2)
        '
        ' Create KML for Consequences
        '
        strSQL = "SELECT * FROM CONSEQUENCES_DECODE"
        pDataTable = GetDataTableFromDatabase(strGEMDatabase, strSQL, "Consequences")
        Dim pFolder3 As Folder = DataTable2KML(pDocument, pDataTable, "Consequences", "X", "Y")
        pDocument.AddFeature(pFolder3)
        '
        ' Create KML for GED
        '
        strSQL = "SELECT * FROM GED_DECODE"
        pDataTable = GetDataTableFromDatabase(strGEMDatabase, strSQL, "GED")
        Dim pFolder4 As Folder = DataTable2KML(pDocument, pDataTable, "GED", "X", "Y")
        pDocument.AddFeature(pFolder4)
        '
        ' Create KML file and assign the document
        '
        Dim root As Kml = New Kml
        root.Feature = pDocument
        Dim kml As KmlFile = KmlFile.Create(root, False)
        '
        ' Save kmz file
        '
        Dim pKmzFile As KmzFile = KmzFile.Create(kml)
        '
        ' Add images to kmz file
        '
        Dim photosFolder As String = IO.Path.GetDirectoryName(strGEMDatabase) & "\" & IO.Path.GetFileNameWithoutExtension(strGEMDatabase) & "_gemmedia"
        Call AddImagesToKMZ(pKmzFile, pPhotosDataTable, strImagePath, photosFolder, Me.IncludeImages.Checked)
        pKmzFile.Save(kmzFilePath)

    End Sub

    Function GetDataTableFromDatabase(ByVal strGEMDatabase As String, ByVal strSQL As String, ByVal strTableName As String) As DataTable
        '
        ' connect to database
        '
        Dim connection As SQLiteConnection = New SQLiteConnection("Data Source=" & strGEMDatabase)
        connection.Open()
        '
        ' GEM_OBJECT
        '
        Dim cmdSQLite As SQLite.SQLiteCommand = connection.CreateCommand()
        With cmdSQLite
            .CommandType = CommandType.Text
            .CommandText = strSQL
        End With


        Dim pDataTable As New DataTable(strTableName)
        Dim pAdapter As New SQLiteDataAdapter(cmdSQLite)
        pAdapter.Fill(pDataTable)
        connection.Close()
        Return pDataTable

    End Function


    Sub AddImagesToKMZ(ByVal pKmzFile As KmzFile, ByVal pDataTable As DataTable, ByVal strField As String, ByVal photosFolder As String, ByVal embedImages As Boolean)
        '
        ' Name: AddImagesToKMZ
        ' Purpose: To add Iamges referenced in a DataTable to KMZ file
        ' Written: K. Adlam, 4/1/2012
        '
        Dim pHashTable As New Hashtable
        For Each pRow As DataRow In pDataTable.Rows
            GC.Collect()
            GC.WaitForPendingFinalizers()
            Dim strPath As String = photosFolder & "\" & pRow(strField).ToString
            If (File.Exists(strPath)) Then
                Dim fname As String = IO.Path.GetFileName(strPath)
                If (Not pHashTable.ContainsKey(fname)) Then
                    '
                    ' Add image to zip
                    '
                    If (embedImages) Then
                        pKmzFile.AddFile("Images/" & fname, My.Computer.FileSystem.ReadAllBytes(strPath))
                    End If
                    '
                    ' Create Thumbnail and add to zip
                    '
                    Dim pThumbnail As Image = MakeThumbnail(strPath)
                    pKmzFile.AddFile("Thumbnails/" & fname, ImageToByte(pThumbnail))
                    '
                    ' Add name to hashtable to track any duplicates
                    '
                    pHashTable.Add(fname, "")
                End If
            End If
        Next

    End Sub

    'Function DataTablePlusImage2KML(ByVal document As Document, ByVal pDataTable As DataTable, ByVal strFolder As String, ByVal strImageField As String, ByVal xField As String, ByVal yField As String, Optional ByVal nameField As String = "", Optional ByVal idField As String = "") As Folder
    '    '
    '    ' Name: DataTablePlusImage2KML
    '    ' Purpose: To convert a datatable containing x,y coordinates and other attributes and image
    '    '          To a KML file containing placemarks
    '    ' Written: K. Adlam, 14/12/2011
    '    '
    '    Try
    '        '
    '        ' Get Schema from Datatable and add to document
    '        '
    '        Dim pSchema As Schema = CreateKMLSchemaFromDataTable(pDataTable)
    '        document.AddSchema(pSchema)
    '        '
    '        ' Create Folder
    '        '
    '        Dim pFolder As Folder = New Folder
    '        pFolder.Name = strFolder
    '        '
    '        ' Loop through each row in the DataTable and create Placemarks
    '        '
    '        For Each row As DataRow In pDataTable.Rows
    '            counter = counter + 1
    '            '
    '            ' Get x,y,name etc
    '            '
    '            If (Not (IsDBNull(row(xField)) Or IsDBNull(row(yField)))) Then
    '                Dim xc As Double = row(xField)
    '                Dim yc As Double = row(yField)
    '                Dim name As String = ""
    '                If (nameField <> "") Then name = row(nameField).ToString

    '                Dim id As String = ""
    '                If (idField <> "") Then id = row(idField).ToString
    '                '
    '                ' Create point
    '                '
    '                Dim point As New Point()
    '                point.Coordinate = New Vector(yc, xc)
    '                '
    '                ' Create placemark
    '                '
    '                Dim placemark As New Placemark()
    '                placemark.Geometry = point
    '                If (name <> "") Then placemark.Name = name
    '                If (id <> "") Then placemark.Id = id
    '                '
    '                ' description
    '                '
    '                Dim pdescription As Description = New Description
    '                pdescription.Text = MakeTableDescription(row, strImageField, Me.IncludeImages.Checked)
    '                placemark.Description = pdescription
    '                '
    '                ' Create icon
    '                '
    '                Dim thumbnailPath As String = "Thumbnails/" & IO.Path.GetFileName(row(strImageField).ToString)
    '                Dim pStyle As Style = New Style
    '                pStyle.Id = "Thumbnail" & CStr(counter)
    '                pStyle.Icon = New IconStyle()
    '                pStyle.Icon.Icon = New IconStyle.IconLink(New Uri(thumbnailPath, UriKind.Relative))
    '                pStyle.Icon.Scale = 1.0
    '                placemark.StyleUrl = New Uri("#Thumbnail" & CStr(counter), UriKind.Relative)
    '                document.AddStyle(pStyle)
    '                '
    '                ' Add placemark to the folder
    '                '
    '                pFolder.AddFeature(placemark)
    '                '
    '            End If
    '        Next
    '        Return pFolder

    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        Return Nothing
    '    End Try
    '    '
    'End Function


    Function DataTable2KML(ByVal document As Document, ByVal pDataTable As DataTable, ByVal strFolder As String, ByVal xField As String, ByVal yField As String, Optional ByVal nameField As String = "", Optional ByVal idField As String = "", Optional ByVal iconURL As String = "") As Folder
        '
        ' Name: DataTable2KML
        ' Purpose: To convert a datatable containing x,y coordinates and other attributes
        '          To a KML file containing placemarks
        ' Written: K. Adlam, 14/12/2011
        '
        Try
            '
            ' Create Folder
            '
            Dim pFolder As Folder = New Folder
            pFolder.Name = strFolder
            '
            ' Create icon
            '
            Dim iconID As String = ""
            If (iconURL <> "") Then
                counter = counter + 1
                iconID = "ICO" & counter
                Dim pStyle As Style = New Style
                pStyle.Id = iconID
                pStyle.Icon = New IconStyle()
                pStyle.Icon.Icon = New IconStyle.IconLink(New Uri(iconURL, UriKind.Absolute))
                pStyle.Icon.Scale = 1.0
                document.AddStyle(pStyle)
            End If
            '
            ' Loop through each row in the DataTable and create Placemarks
            '
            For Each row As DataRow In pDataTable.Rows
                '
                ' Get x,y,name etc
                '
                If (Not (IsDBNull(row(xField)) Or IsDBNull(row(yField)))) Then
                    Dim xc As Double = row(xField)
                    Dim yc As Double = row(yField)
                    Dim name As String = ""
                    If (nameField <> "") Then name = row(nameField).ToString

                    Dim id As String = ""
                    If (idField <> "") Then id = row(idField).ToString
                    '
                    ' Create point
                    '
                    Dim point As New Point()
                    point.Coordinate = New Vector(yc, xc)
                    '
                    ' Create placemark
                    '
                    Dim placemark As New Placemark()
                    placemark.Geometry = point
                    If (name <> "") Then placemark.Name = name
                    If (id <> "") Then placemark.Id = id
                    If (iconURL <> "") Then placemark.StyleUrl = New Uri("#" & iconID, UriKind.Relative)
                    '
                    ' description
                    '
                    Dim pdescription As Description = New Description
                    pdescription.Text = MakeTableDescription(row, "FILENAME", Me.IncludeImages.Checked, strFolder)
                    placemark.Description = pdescription
                    '
                    ' Add placemark to the folder
                    '
                    pFolder.AddFeature(placemark)
                    '
                End If
            Next
            Return pFolder

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
        '
    End Function

    Function DataTable2KMLExt(ByVal document As Document, ByVal pDataTable As DataTable, ByVal strFolder As String, ByVal xField As String, ByVal yField As String, Optional ByVal nameField As String = "", Optional ByVal idField As String = "", Optional ByVal iconURL As String = "") As Folder
        '
        ' Name: DataTable2KML
        ' Purpose: To convert a datatable containing x,y coordinates and other attributes
        '          To a KML file containing placemarks
        ' Written: K. Adlam, 14/12/2011
        '
        Try
            '
            ' Create Folder
            '
            Dim pFolder As Folder = New Folder
            pFolder.Name = strFolder
            '
            ' Create icon
            '
            Dim iconID As String = ""
            If (iconURL <> "") Then
                counter = counter + 1
                iconID = "ICO" & counter
                Dim pStyle As Style = New Style
                pStyle.Id = iconID
                pStyle.Icon = New IconStyle()
                pStyle.Icon.Icon = New IconStyle.IconLink(New Uri(iconURL, UriKind.Absolute))
                pStyle.Icon.Scale = 1.0
                document.AddStyle(pStyle)
            End If
            '
            ' Loop through each row in the DataTable and create Placemarks
            '
            For Each row As DataRow In pDataTable.Rows
                '
                ' Get x,y,name etc
                '
                Dim xc As Double = row(xField)
                Dim yc As Double = row(yField)
                Dim name As String = ""
                If (nameField <> "") Then name = row(nameField).ToString

                Dim id As String = ""
                If (idField <> "") Then id = row(idField).ToString
                '
                ' Create point
                '
                Dim point As New Point()
                point.Coordinate = New Vector(yc, xc)
                '
                ' Create placemark
                '
                Dim placemark As New Placemark()
                placemark.Geometry = point
                If (name <> "") Then placemark.Name = name
                If (id <> "") Then placemark.Id = id
                If (iconURL <> "") Then placemark.StyleUrl = New Uri("#" & iconID, UriKind.Relative)
                '
                ' Create Extended Data
                'Loop through columns and add attributes to extended data
                ' 
                Dim pExtended As ExtendedData = New ExtendedData
                For Each col As DataColumn In pDataTable.Columns
                    Dim pData As SharpKml.Dom.Data = New SharpKml.Dom.Data
                    pData.Name = col.ColumnName
                    pData.Value = row(col).ToString
                    pExtended.AddData(pData)
                Next
                placemark.ExtendedData = pExtended
                '
                ' Add placemark to the folder
                '
                pFolder.AddFeature(placemark)
                '
            Next
            Return pFolder

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
        '
    End Function

    Function MakeSimpleField(ByVal strName As String, ByVal strType As String) As SimpleField
        Dim pSimpleField As SimpleField = New SimpleField
        pSimpleField.Name = strName
        pSimpleField.FieldType = strType
        Return pSimpleField
    End Function

    Function MakeTableDescription(ByVal pRow As DataRow, ByVal strImageField As String, ByVal embedImages As Boolean, ByVal strFolder As String) As String
        '
        ' Name: MakeDescription
        ' Purpose: To create a description from the contents of a DataTable
        ' Written: K. Adlam, 14/12/2011
        '
        Dim html As StringBuilder = New StringBuilder
        html.Append("<![CDATA[")
        '
        ' Image if present
        '


        '
        ' Attribute table
        '
        html.Append("<center><table><tr><th colspan='2' align='center'><em>Attributes</em></th></tr>")
        '
        ' Loop through columns and add attributes to extended data
        '
        For Each col As DataColumn In pRow.Table.Columns
            If Not IsDBNull(pRow(col)) Then
                If pRow(col).ToString <> "" Then
                    If Not col.ColumnName.EndsWith("_UID") Then
                        html.Append("<tr bgcolor=""#E3E3F3"">")
                        html.Append("<th>")
                        html.Append(col.ColumnName)
                        html.Append("</th>")
                        html.Append("<td>")
                        html.Append(WebUtility.HtmlEncode(pRow(col).ToString))
                        html.Append("</td>")
                        html.Append("</tr>")
                    End If
                End If
            End If
        Next

        html.Append("</table></center>")

        If strFolder = "Buildings" Then
            Dim strSQL As String = "SELECT * FROM MEDIA_DETAIL_DECODE WHERE (MEDIA_TYPE='Photograph' OR MEDIA_TYPE='Sketch') AND GEMOBJ_UID='" & pRow("OBJ_UID").ToString & "'"
            Dim pPhotosDataTable As DataTable = GetDataTableFromDatabase(pStrDB, strSQL, "Photographs")
            strImageField = "FILENAME"
            For Each row As DataRow In pPhotosDataTable.Rows
                If (strImageField <> "") Then
                    html.Append("<p/>")
                    If Not IsDBNull(row(strImageField)) Then
                        If (embedImages) Then
                            html.Append("<img src=""./Images/" & IO.Path.GetFileName(row(strImageField)).ToString.ToLower)
                        Else
                            html.Append("<img src=""../Photographs/" & IO.Path.GetFileName(row(strImageField)).ToString.ToLower)
                        End If
                        html.Append(""" width=""300""/>")
                        html.Append("<br/>" & row("COMMENTS").ToString)
                    End If
                End If
            Next
        End If
        html.Append("]]>")
        Return html.ToString

    End Function

    'Function MakeTableDescription(ByVal pRow As DataRow) As String
    '    '
    '    ' Name: MakeTableDescription
    '    ' Purpose: To create a description from the contents of a DataTable
    '    ' Written: K. Adlam, 14/12/2011
    '    '
    '    Dim html As StringBuilder = New StringBuilder
    '    html.Append("<![CDATA[")
    '    '
    '    ' Attribute table
    '    '
    '    html.Append("<center><table><tr><th colspan='2' align='center'><em>Attributes</em></th></tr>")
    '    '
    '    ' Loop through columns and add attributes to extended data
    '    '
    '    For Each col As DataColumn In pRow.Table.Columns
    '        If pRow(col).ToString <> "" Then
    '            html.Append("<tr bgcolor=""#E3E3F3"">")
    '            html.Append("<th>")
    '            html.Append(col.ColumnName)
    '            html.Append("</th>")
    '            html.Append("<td>")
    '            html.Append(pRow(col).ToString)
    '            html.Append("</td>")
    '            html.Append("</tr>")
    '        End If
    '    Next

    '    html.Append("</table></center>]]>")

    '    Return html.ToString

    'End Function

    Private Function ImageToByte(ByVal img As Image) As Byte()

        Dim imgStream As MemoryStream = New MemoryStream()
        img.Save(imgStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        imgStream.Close()
        Dim byteArray As Byte() = imgStream.ToArray()
        imgStream.Dispose()
        Return byteArray

    End Function

    Function MakeThumbnail(ByVal strPath As String) As Image
        '
        ' Name: MakeThumbnail
        ' Purpose: To create a thumbnail image from main image
        ' Written: K. Adlam, 14/12/2011
        '
        Dim pThumb As Image = Nothing
        Try
            Dim pImage As Image = Image.FromFile(strPath)
            If Not pImage Is Nothing Then
                pThumb = pImage.GetThumbnailImage(100, 100, Nothing, New IntPtr())
            End If
        Catch
            MsgBox("Problem creating thumbnail image")
        End Try

        Return pThumb

    End Function

    Function CreateKMLSchemaFromDataTable(ByVal pDataTable As DataTable) As Schema
        '
        ' Name: CreateKMLSchemaFromDataTable
        ' Purpose: To create a KML Schema from a DataTable Schema
        ' Written: K. Adlam, 14/12/2011
        '
        Dim pSchema As Schema = New Schema
        pSchema.Name = pDataTable.TableName
        pSchema.Id = pDataTable.TableName
        For Each pColumn As DataColumn In pDataTable.Columns
            pSchema.AddField(MakeSimpleField(pColumn.ColumnName, "string"))
        Next

        Return pSchema
    End Function

    Private Sub SourceBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SourceBrowse.Click
        With OpenFileDialog1
            .FileName = ""
            .Filter = "GEM database files (*.db3)|*.db3|" & "All files|*.*"
            If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Me.GEMDatabase.Text = .FileName
            End If
        End With
    End Sub


    Private Sub KMLBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KMLBrowse.Click
        With SaveFileDialog1
            .FileName = ""
            .Filter = "KMZ files (*.kmz)|*.kmz|" & "All files|*.*"
            If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Me.KMZFilePath.Text = .FileName
            End If
        End With
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Dispose()
    End Sub

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ok.Click

        Try
            '
            ' Refresh database
            '
            If (Not gemdb Is Nothing) Then Call gemdb.RefreshGEMDataTableContents()
            '
            ' Check input for blanks
            '
            If (Me.GEMDatabase.Text = "" Or Me.KMZFilePath.Text = "") Then Exit Sub
            '
            ' Check GEM database exists
            '
            If (Not IO.File.Exists(Me.GEMDatabase.Text)) Then
                MsgBox("GEM database " & Me.GEMDatabase.Text & " does not exist")
                Exit Sub
            End If
            '
            ' Check Folder exists if not try to create one
            '
            Dim outFolder As String = IO.Path.GetDirectoryName(Me.KMZFilePath.Text)
            If (Not IO.Directory.Exists(outFolder)) Then
                IO.Directory.CreateDirectory(outFolder)
            End If
            '
            ' Make the KMZ file
            '
            Dim kmzPath As String = IO.Path.ChangeExtension(Me.KMZFilePath.Text, ".kmz") 'force extension
            Call MakeKMZ(Me.GEMDatabase.Text, kmzPath)

            MessageBox.Show("Export Completed Successfully", "Export Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Me.Dispose()

    End Sub



End Class
