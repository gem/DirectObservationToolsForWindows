Imports System.Collections.Generic
Imports System.Linq

'http://www.codeproject.com/Articles/36869/Fuzzy-Search

Public Class MatchedWord
    Public Word As String
    Public Score As Double
End Class


''' <summary>
''' Provides methods for fuzzy string searching.
''' </summary>
Public Class FuzzySearch
    ''' <summary>
    ''' Calculates the Levenshtein-distance of two strings.
    ''' </summary>
    ''' <param name="src">
    ''' 1. string
    ''' </param>
    ''' <param name="dest">
    ''' 2. string
    ''' </param>
    ''' <returns>
    ''' Levenshstein-distance
    ''' </returns>
    ''' <remarks>
    ''' See 
    ''' <a href='http://en.wikipedia.org/wiki/Levenshtein_distance'>
    ''' http://en.wikipedia.org/wiki/Levenshtein_distance
    ''' </a>
    ''' </remarks>
    Public Shared Function LevenshteinDistance(ByVal src As String, ByVal dest As String) As Integer
        Dim d As Integer(,) = New Integer(src.Length, dest.Length) {}
        Dim i As Integer, j As Integer, cost As Integer
        Dim str1 As Char() = src.ToCharArray()
        Dim str2 As Char() = dest.ToCharArray()

        For i = 0 To str1.Length
            d(i, 0) = i
        Next
        For j = 0 To str2.Length
            d(0, j) = j
        Next
        For i = 1 To str1.Length
            For j = 1 To str2.Length

                If str1(i - 1) = str2(j - 1) Then
                    cost = 0
                Else
                    cost = 1
                End If

                ' Deletion
                ' Insertion
                d(i, j) = Math.Min(d(i - 1, j) + 1, Math.Min(d(i, j - 1) + 1, d(i - 1, j - 1) + cost))
                ' Substitution
                If (i > 1) AndAlso (j > 1) AndAlso (str1(i - 1) = str2(j - 2)) AndAlso (str1(i - 2) = str2(j - 1)) Then
                    d(i, j) = Math.Min(d(i, j), d(i - 2, j - 2) + cost)
                End If
            Next
        Next

        Return d(str1.Length, str2.Length)
    End Function
    '---------------------------------------------------------------------
    ''' <summary>
    ''' Fuzzy searches a list of strings.
    ''' </summary>
    ''' <param name="word">
    ''' The word to find.
    ''' </param>
    ''' <param name="wordList">
    ''' A list of word to be searched.
    ''' </param>
    ''' <param name="fuzzyness">
    ''' Ration of the fuzzyness. A value of 0.8 means that the 
    ''' difference between the word to find and the found words
    ''' is less than 20%.
    ''' </param>
    ''' <returns>
    ''' The list with the found words.
    ''' </returns>
    ''' <example>
    ''' 
    ''' </example>
    Public Shared Function Search(ByVal word As String, ByVal wordList As List(Of String), ByVal fuzzyness As Double) As String
        Dim foundWords As New List(Of MatchedWord)()

        For Each s As String In wordList
            ' Calculate the Levenshtein-distance:

            Dim f As String = IO.Path.GetFileNameWithoutExtension(s)

            Dim levenshteinDistance__1 As Integer = LevenshteinDistance(word, f)

            ' Length of the longer string:
            Dim length As Integer = Math.Max(word.Length, f.Length)

            ' Calculate the score:
            Dim score As Double = 1.0 - CDbl(levenshteinDistance__1) / length

            ' Match?
            If score > fuzzyness Then
                Dim mw As New MatchedWord
                mw.Word = s
                mw.Score = score
                foundWords.Add(mw)
            End If
        Next

        Dim topScore As Double = 0
        Dim topWord As String = ""
        For Each d As MatchedWord In foundWords
            If d.Score > topScore Then
                topScore = d.Score
                topWord = d.Word
            End If
        Next

        Return topWord
    End Function
End Class

