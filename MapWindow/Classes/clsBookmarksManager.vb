'********************************************************************************************************
'File Name: frmBookmarkManager.vb
'Description: Bookmark Manager.
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'
'The Original Code is MapWindow Open Source. 
'
'The Initial Developer of this version of the Original Code is Daniel P. Ames using portions created by 
'Utah State University and the Idaho National Engineering and Environmental Lab that were released as 
'public domain in March 2004.  
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 

'9/3/2010 - Christian Degrassi: Implemented "Bookmark Manager" Enhancement. Issue 1634, 1639
'********************************************************************************************************

Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports System.Xml

Public Class BookmarksManager

    <XmlRoot("Extents")> Public Class Extents

        Private _xMin As Double
        Private _yMin As Double
        Private _xMax As Double
        Private _yMax As Double

        <XmlAttribute()> Public Property xMin() As Double
            Get
                Return _xMin
            End Get
            Set(ByVal value As Double)
                _xMin = value
            End Set
        End Property

        <XmlAttribute()> Public Property xMax() As Double
            Get
                Return _xMax
            End Get
            Set(ByVal value As Double)
                _xMax = value
            End Set
        End Property

        <XmlAttribute()> Public Property yMin() As Double
            Get
                Return _yMin
            End Get
            Set(ByVal value As Double)
                _yMin = value
            End Set
        End Property

        <XmlAttribute()> Public Property yMax() As Double
            Get
                Return _yMax
            End Get
            Set(ByVal value As Double)
                _yMax = value
            End Set
        End Property

        Public Sub New()

        End Sub

        Public Sub New(ByVal NewExtents As MapWinGIS.Extents)
            If (NewExtents IsNot Nothing) Then
                _xMin = NewExtents.xMin
                _xMax = NewExtents.xMax
                _yMin = NewExtents.yMin
                _yMax = NewExtents.yMax
            End If
        End Sub

    End Class

    <XmlRoot("Bookmark")> Public Class Bookmark

        Private _Name As String
        Private _Exts As Extents

        <XmlAttribute()> Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Extents() As Extents
            Get
                Return _Exts
            End Get
            Set(ByVal value As Extents)
                If (value IsNot Nothing) Then
                    _Exts.xMin = value.xMin
                    _Exts.yMin = value.yMin
                    _Exts.xMax = value.xMax
                    _Exts.yMax = value.yMax
                End If
            End Set
        End Property

        Public Sub New()
            _Name = String.Empty
            _Exts = New Extents
        End Sub

        Public Sub New(ByVal BookmarkName As String, ByVal BookmarkExts As MapWinGIS.Extents)
            _Name = BookmarkName
            _Exts = New Extents
            If (BookmarkExts IsNot Nothing) Then
                _Exts.xMin = BookmarkExts.xMin
                _Exts.yMin = BookmarkExts.yMin
                _Exts.xMax = BookmarkExts.xMax
                _Exts.yMax = BookmarkExts.yMax
            End If
        End Sub

        Public Sub New(ByVal BookmarkName As String, ByVal BookmarkExts As Extents)
            _Name = BookmarkName
            _Exts = New Extents
            If (BookmarkExts IsNot Nothing) Then
                _Exts.xMin = BookmarkExts.xMin
                _Exts.yMin = BookmarkExts.yMin
                _Exts.xMax = BookmarkExts.xMax
                _Exts.yMax = BookmarkExts.yMax
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return (_Name + " (" & _Exts.xMin.ToString() + ", " + _Exts.xMax.ToString() + "), (" + _Exts.yMin.ToString() + ", " + _Exts.yMax.ToString() + ")")
        End Function

    End Class

    <XmlRoot("Bookmarks")> Public Class Bookmarks
        Implements IEnumerable(Of Bookmark)

        Private _Bookmarks As List(Of Bookmark)

        Default Public Property Items(ByVal Index As Integer) As Bookmark
            Get
                If (Index >= _Bookmarks.Count Or Index < 0) Then
                    Throw New IndexOutOfRangeException
                End If
                Return _Bookmarks(Index)
            End Get
            Set(ByVal value As Bookmark)
                If (Index >= _Bookmarks.Count Or Index < 0 Or value Is Nothing) Then
                    Throw New IndexOutOfRangeException
                End If
                _Bookmarks(Index) = value
            End Set
        End Property

        Public Sub Add(ByVal NewBookmark As Bookmark)
            _Bookmarks.Add(NewBookmark)
        End Sub

        Public Sub Remove(ByVal TargetBookmark As Bookmark)
            _Bookmarks.Remove(TargetBookmark)
        End Sub

        Public Sub RemoveAt(ByVal Index As Integer)
            If (Index >= _Bookmarks.Count Or Index < 0) Then
                Throw New IndexOutOfRangeException
            End If
            _Bookmarks.RemoveAt(Index)
        End Sub

        <XmlIgnore()> Public ReadOnly Property Count()
            Get
                Return _Bookmarks.Count
            End Get
        End Property

        Public Sub New()
            _Bookmarks = New List(Of Bookmark)
        End Sub

        Public Sub New(ByVal BookmarksArrayList As ArrayList)

            _Bookmarks = New List(Of Bookmark)

            For Each bkm As XmlProjectFile.BookmarkedView In BookmarksArrayList
                _Bookmarks.Add(New Bookmark(bkm.Name, bkm.Exts))
            Next

        End Sub

        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of Bookmark) Implements System.Collections.Generic.IEnumerable(Of Bookmark).GetEnumerator
            Return _Bookmarks.GetEnumerator()
        End Function

        Public Function GetDefaultEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return _Bookmarks.GetEnumerator()
        End Function

    End Class

    Public Interface IBookmarkExporter
        WriteOnly Property Bookmarks() As Bookmarks
        Function Save(ByVal Path As String) As Boolean
    End Interface

    Public Interface IBookmarkImporter
        ReadOnly Property Bookmarks() As Bookmarks
        Function Open(ByVal Path As String) As Boolean
    End Interface

    Public Class BookmarkExportXML
        Implements IBookmarkExporter

        Private _Bookmarks As Bookmarks

        Public WriteOnly Property Bookmarks() As Bookmarks Implements IBookmarkExporter.Bookmarks
            Set(ByVal value As Bookmarks)
                _Bookmarks = value
            End Set
        End Property

        Public Function Save(ByVal Path As String) As Boolean Implements IBookmarkExporter.Save

            Dim BookmarksSerializer As XmlSerializer = Nothing
            Dim BookmarksStreamWriter As System.IO.StreamWriter = Nothing
            Dim Result As Boolean = True
            Dim BookmarkNamespaces As XmlSerializerNamespaces = Nothing

            Try
                BookmarkNamespaces = New XmlSerializerNamespaces()
                BookmarkNamespaces.Add(String.Empty, String.Empty)
                BookmarksSerializer = New XmlSerializer(GetType(Bookmarks))
                BookmarksStreamWriter = New System.IO.StreamWriter(Path)
                BookmarksSerializer.Serialize(BookmarksStreamWriter, _Bookmarks, BookmarkNamespaces)
            Catch ex As Exception
                Result = False
                If (System.IO.File.Exists(Path)) Then
                    BookmarksStreamWriter.Close()
                    System.IO.File.Delete(Path)
                End If
                Debug.Print(ex.Message)
            Finally
                If (BookmarksStreamWriter IsNot Nothing) Then
                    BookmarksStreamWriter.Close()
                End If
            End Try

            Return Result

        End Function

    End Class

    Public Class BookmarkExportCSV
        Implements IBookmarkExporter

        Private _Bookmarks As Bookmarks
        Private _HasHeaders As Boolean

        Public WriteOnly Property Bookmarks() As Bookmarks Implements IBookmarkExporter.Bookmarks
            Set(ByVal value As Bookmarks)
                _Bookmarks = value
            End Set
        End Property

        Public Property HasHeaders() As Boolean
            Get
                Return _HasHeaders
            End Get
            Set(ByVal value As Boolean)
                _HasHeaders = value
            End Set
        End Property

        Public Sub New()
            _HasHeaders = False
        End Sub

        Public Function Save(ByVal Path As String) As Boolean Implements IBookmarkExporter.Save

            Dim BookmarksCSVText As System.Text.StringBuilder = New System.Text.StringBuilder(String.Empty)
            Dim Result As Boolean = True

            Try
                If (_HasHeaders) Then
                    BookmarksCSVText.AppendLine("Name, xMin, yMin, xMax, yMax")
                End If

                For Each bkm As Bookmark In _Bookmarks
                    BookmarksCSVText.AppendLine(bkm.Name + ", " + bkm.Extents.xMin.ToString() + ", " + bkm.Extents.yMin.ToString() + ", " + bkm.Extents.xMax.ToString() + ", " + bkm.Extents.yMax.ToString())
                Next

                My.Computer.FileSystem.WriteAllText(Path, BookmarksCSVText.ToString(), False)
            Catch ex As Exception
                Result = False
                Debug.Print(ex.Message)
            End Try

            Return Result

        End Function

    End Class

    Public Class BookmarkImportXML
        Implements IBookmarkImporter

        Private _Bookmarks As Bookmarks

        Public ReadOnly Property Bookmarks() As Bookmarks Implements IBookmarkImporter.Bookmarks
            Get
                Return _Bookmarks
            End Get
        End Property

        Public Sub New()
            _Bookmarks = New Bookmarks
        End Sub

        Public Function Open(ByVal Path As String) As Boolean Implements IBookmarkImporter.Open

            Dim BookmarksSerializer As XmlSerializer = Nothing
            Dim BookmarksStreamReader As System.IO.StreamReader = Nothing
            Dim Result As Boolean = True

            Try
                BookmarksSerializer = New XmlSerializer(GetType(Bookmarks))
                BookmarksStreamReader = New System.IO.StreamReader(Path)
                _Bookmarks = CType(BookmarksSerializer.Deserialize(BookmarksStreamReader), Bookmarks)
            Catch ex As Exception
                Result = False
                Debug.Print(ex.Message)
                _Bookmarks = Nothing
            Finally
                If (BookmarksStreamReader IsNot Nothing) Then
                    BookmarksStreamReader.Close()
                End If
            End Try

            Return Result

        End Function

    End Class

    Public Class BookmarkImportCSV
        Implements IBookmarkImporter

        Private _HasHeader As Boolean
        Private _Bookmarks As Bookmarks

        Public Property HasHeader() As Boolean
            Get
                Return _HasHeader
            End Get
            Set(ByVal value As Boolean)
                _HasHeader = value
            End Set
        End Property

        Public ReadOnly Property Bookmarks() As Bookmarks Implements IBookmarkImporter.Bookmarks
            Get
                Return _Bookmarks
            End Get
        End Property

        Public Sub New()
            _HasHeader = False
            _Bookmarks = New Bookmarks
        End Sub

        Public Function Open(ByVal Path As String) As Boolean Implements IBookmarkImporter.Open

            Dim BookmarkCSVText As System.Text.StringBuilder = Nothing
            Dim BookmarkStringReader As System.IO.TextReader = Nothing
            Dim BookmarkLineObject As Object = Nothing
            Dim BookmarkLine As String
            Dim Result As Boolean = True

            Dim Bookmark As Bookmark = Nothing

            Try
                BookmarkCSVText = New System.Text.StringBuilder(String.Empty)
                BookmarkCSVText.AppendLine(My.Computer.FileSystem.ReadAllText(Path))
                BookmarkStringReader = New System.IO.StringReader(BookmarkCSVText.ToString())
                BookmarkLineObject = BookmarkStringReader.ReadLine()

                If (_HasHeader) Then
                    BookmarkLineObject = BookmarkStringReader.ReadLine()
                End If

                Do While (BookmarkLineObject IsNot Nothing)

                    BookmarkLine = BookmarkLineObject.ToString
                    If (BookmarkLine.Trim <> String.Empty) Then
                        Bookmark = New Bookmark()
                        Bookmark.Name = BookmarkLine.Split(",")(0)
                        Bookmark.Extents.xMin = Double.Parse(BookmarkLine.Split(",")(1))
                        Bookmark.Extents.yMin = Double.Parse(BookmarkLine.Split(",")(2))
                        Bookmark.Extents.xMax = Double.Parse(BookmarkLine.Split(",")(3))
                        Bookmark.Extents.yMax = Double.Parse(BookmarkLine.Split(",")(4))
                        _Bookmarks.Add(Bookmark)
                    End If

                    BookmarkLineObject = BookmarkStringReader.ReadLine()
                Loop

            Catch ex As Exception
                Result = False
                Debug.Print(ex.Message)
                _Bookmarks = Nothing
            Finally
                If (BookmarkStringReader IsNot Nothing) Then
                    BookmarkStringReader.Close()
                End If
            End Try

            Return Result

        End Function

    End Class

End Class
