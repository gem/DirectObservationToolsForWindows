Imports System.IO
Imports HtmlAgilityPack
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1


    Sub DoFiles(ByVal strInFolder As String, ByVal strOutFolder As String, ByVal strTemplate As String)
        '
        ' Name: DoFiles
        ' Purpose: To Create new GEM help files by extracting and editing the required html nodes.
        '          Files are output to a new folder
        ' Written: K.Adlam, December 2012
        '
        '
        ' Check output folder exists and get as DirectoryInfo
        '
        If (Not IO.Directory.Exists(strOutFolder)) Then IO.Directory.CreateDirectory(strOutFolder)
        Dim pDir As New DirectoryInfo(strInFolder)
        '
        ' Loop through all html files in the directory
        '
        For Each fl In pDir.GetFiles("*.html", SearchOption.TopDirectoryOnly)
            Dim inPath As String = fl.FullName
            Dim outPath As String = strOutFolder & "\" & (IO.Path.GetFileName(inPath).Replace("-2", ""))
            Call MakeHelpFile(strTemplate, inPath, outPath)
        Next

    End Sub

    Sub MakeHelpFile(ByVal strTemplate As String, ByVal strInFile As String, ByVal strOutfile As String)
        '
        ' Name: MakeHelpFile
        ' Purpose: To make a new GEM help file by extracting and editing html from an existing GEM helpfile
        ' Written: K.Adlam, December 2012
        '
        '
        ' Load Template
        '
        Dim htmlTemplateDoc As HtmlDocument = New HtmlDocument
        htmlTemplateDoc.OptionWriteEmptyNodes = True
        If (HtmlNode.ElementsFlags.ContainsKey("img")) Then
            HtmlNode.ElementsFlags("img") = HtmlElementFlag.Closed
        Else
            HtmlNode.ElementsFlags.Add("img", HtmlElementFlag.Closed)
        End If

        htmlTemplateDoc.Load(strTemplate, True)
        '
        ' Load In file
        '
        Dim htmlInDoc As HtmlDocument = New HtmlDocument
        htmlInDoc.Load(strInFile, True)
        '
        ' Import Title
        '
        Dim pElementIn As HtmlNode = htmlInDoc.GetElementbyId("parent-fieldname-title")
        If (pElementIn Is Nothing) Then Exit Sub

        Dim pTemplateElement As HtmlNode = htmlTemplateDoc.GetElementbyId("parent-fieldname-title")
        pTemplateElement.InnerHtml = pElementIn.InnerHtml
        '
        ' Import Definition
        '
        pElementIn = htmlInDoc.GetElementbyId("parent-fieldname-definition")
        If (pElementIn Is Nothing) Then Exit Sub
        pTemplateElement = htmlTemplateDoc.GetElementbyId("parent-fieldname-definition")
        Dim html As String = pElementIn.InnerHtml
        html = html.Replace("../", "../../")
        'html = html.Replace(Chr(194) & Chr(160), "&nbsp;")

        pTemplateElement.InnerHtml = html
        '
        ' Save new html file
        '
        htmlTemplateDoc.Save(strOutfile, Encoding.GetEncoding("utf-8"))

    End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    'MsgBox(Levenshtein("testing-keith", "keith-testing"))
    '    'MsgBox(Levenshtein("testing-keith", "testing-keithb"))
    '    MsgBox(LongestCommonSubstring("testing-keith", "keith-testing"))
    'End Sub


    Public Function Levenshtein(ByVal s1 As String, ByVal s2 As String)

        Dim i As Integer
        Dim j As Integer
        Dim l1 As Integer
        Dim l2 As Integer
        Dim d(1, 1) As Integer
        Dim min1 As Integer
        Dim min2 As Integer

        l1 = Len(s1)
        l2 = Len(s2)
        ReDim d(l1, l2)
        For i = 0 To l1
            d(i, 0) = i
        Next
        For j = 0 To l2
            d(0, j) = j
        Next
        For i = 1 To l1
            For j = 1 To l2
                If Mid(s1, i, 1) = Mid(s2, j, 1) Then
                    d(i, j) = d(i - 1, j - 1)
                Else
                    min1 = d(i - 1, j) + 1
                    min2 = d(i, j - 1) + 1
                    If min2 < min1 Then
                        min1 = min2
                    End If
                    min2 = d(i - 1, j - 1) + 1
                    If min2 < min1 Then
                        min1 = min2
                    End If
                    d(i, j) = min1
                End If
            Next
        Next
        Levenshtein = d(l1, l2)
    End Function

    Public Function LongestCommonSubstring(ByVal s1 As String, ByVal s2 As String) As Integer
        Dim num(s1.Length - 1, s2.Length - 1) As Integer   '2D array
        Dim letter1 As Char = Nothing
        Dim letter2 As Char = Nothing
        Dim len As Integer = 0
        Dim ans As Integer = 0
        For i As Integer = 0 To s1.Length - 1
            For j As Integer = 0 To s2.Length - 1
                letter1 = s1.Chars(i)
                letter2 = s2.Chars(j)
                If Not letter1.Equals(letter2) Then
                    num(i, j) = 0
                Else
                    If i.Equals(0) Or j.Equals(0) Then
                        num(i, j) = 1
                    Else
                        num(i, j) = 1 + num(i - 1, j - 1)
                    End If
                    If num(i, j) > len Then
                        len = num(i, j)
                        ans = num(i, j)
                    End If
                End If
            Next j
        Next i
        Return ans
    End Function


    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Dispose()
    End Sub

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ok.Click

        Call DoFiles("E:\GEM\GEM_HELP\glossary", "E:\GEM\GEM_HELP\glossary_New", "E:\GEM\GEM_HELP\Template.html")
        MsgBox("Done")
    End Sub
End Class

