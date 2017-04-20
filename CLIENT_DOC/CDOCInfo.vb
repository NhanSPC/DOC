
Imports pbs.Helper
Imports pbs.Helper.Interfaces
Imports System.Data
Imports Csla
Imports Csla.Data
Imports Csla.Validation



<Serializable()> _
Public Class CDOCInfo
    Inherits Csla.ReadOnlyBase(Of CDOCInfo)
    Implements IComparable
    Implements IInfo
    Implements IDocLink
    'Implements IInfoStatus


#Region " Business Properties and Methods "


    Private _lineNo As Integer
    Public ReadOnly Property LineNo() As Integer
        Get
            Return _lineNo
        End Get
    End Property

    Private _submitTime As String = String.Empty
    Public ReadOnly Property SubmitTime() As String
        Get
            Return _submitTime
        End Get
    End Property

    Private _workStation As String = String.Empty
    Public ReadOnly Property WorkStation() As String
        Get
            Return _workStation
        End Get
    End Property

    Private _userId As String = String.Empty
    Public ReadOnly Property UserId() As String
        Get
            Return _userId
        End Get
    End Property

    Private _localDocUrl As String = String.Empty
    Public ReadOnly Property LocalDocUrl() As String
        Get
            Return _localDocUrl
        End Get
    End Property

    Private _cloundDocUrl As String = String.Empty
    Public ReadOnly Property CloundDocUrl() As String
        Get
            Return _cloundDocUrl
        End Get
    End Property

    Private _processingStatus As String = String.Empty
    Public ReadOnly Property ProcessingStatus() As String
        Get
            Return _processingStatus
        End Get
    End Property

    Private _reference As String = String.Empty
    Public ReadOnly Property Reference() As String
        Get
            Return _reference
        End Get
    End Property

    Private _descriptn As String = String.Empty
    Public ReadOnly Property Descriptn() As String
        Get
            Return _descriptn
        End Get
    End Property

    Private _clientCode As String = String.Empty
    Public ReadOnly Property ClientCode() As String
        Get
            Return _clientCode
        End Get
    End Property

    Private _empCode As String = String.Empty
    Public ReadOnly Property EmpCode() As String
        Get
            Return _empCode
        End Get
    End Property

    Private _projectCode As String = String.Empty
    Public ReadOnly Property ProjectCode() As String
        Get
            Return _projectCode
        End Get
    End Property

    Private _docType As String = String.Empty
    Public ReadOnly Property DocType() As String
        Get
            Return _docType
        End Get
    End Property

    Private _docDate As pbs.Helper.SmartDate = New pbs.Helper.SmartDate()
    Public ReadOnly Property DocDate() As String
        Get
            Return _docDate.DateViewFormat
        End Get
    End Property

    Private _contractCode As String = String.Empty
    Public ReadOnly Property ContractCode() As String
        Get
            Return _contractCode
        End Get
    End Property

    Private _updated As pbs.Helper.SmartDate = New pbs.Helper.SmartDate()
    Public ReadOnly Property Updated() As String
        Get
            Return _updated.DateViewFormat
        End Get
    End Property

    Private _updatedBy As String = String.Empty
    Public ReadOnly Property UpdatedBy() As String
        Get
            Return _updatedBy
        End Get
    End Property

    'Get ID
    Protected Overrides Function GetIdValue() As Object
        Return _lineNo
    End Function

    'IComparable
    Public Function CompareTo(ByVal IDObject) As Integer Implements System.IComparable.CompareTo
        Dim ID = IDObject.ToString
        Dim pLineNo As Integer = ID.Trim.ToInteger
        If _lineNo < pLineNo Then Return -1
        If _lineNo > pLineNo Then Return 1
        Return 0
    End Function

    Public ReadOnly Property Code As String Implements IInfo.Code
        Get
            Return _lineNo
        End Get
    End Property

    Public ReadOnly Property LookUp As String Implements IInfo.LookUp
        Get
            Return _lineNo
        End Get
    End Property

    Public ReadOnly Property Description As String Implements IInfo.Description
        Get
            Return _descriptn
        End Get
    End Property


    Public Sub InvalidateCache() Implements IInfo.InvalidateCache
        CDOCInfoList.InvalidateCache()
    End Sub


#End Region 'Business Properties and Methods

#Region " Factory Methods "

    Friend Shared Function GetCDOCInfo(ByVal dr As SafeDataReader) As CDOCInfo
        Return New CDOCInfo(dr)
    End Function

    Friend Shared Function EmptyCDOCInfo(Optional ByVal pLineNo As String = "") As CDOCInfo
        Dim info As CDOCInfo = New CDOCInfo
        With info
            ._lineNo = pLineNo.ToInteger

        End With
        Return info
    End Function

    Private Sub New(ByVal dr As SafeDataReader)
        _lineNo = dr.GetInt32("LINE_NO")
        _submitTime = dr.GetDateTime("SUBMIT_TIME")
        _workStation = dr.GetString("WORK_STATION").TrimEnd
        _userId = dr.GetString("USER_ID").TrimEnd
        _localDocUrl = dr.GetString("LOCAL_DOC_URL").TrimEnd
        _cloundDocUrl = dr.GetString("CLOUND_DOC_URL").TrimEnd
        _processingStatus = dr.GetString("PROCESSING_STATUS").TrimEnd
        _reference = dr.GetString("REFERENCE").TrimEnd
        _descriptn = dr.GetString("DESCRIPTION").TrimEnd
        _clientCode = dr.GetString("CLIENT_CODE").TrimEnd
        _empCode = dr.GetString("EMP_CODE").TrimEnd
        _projectCode = dr.GetString("PROJECT_CODE").TrimEnd
        _docType = dr.GetString("DOC_TYPE").TrimEnd
        _docDate.Text = dr.GetInt32("DOC_DATE")
        _contractCode = dr.GetString("CONTRACT_CODE").TrimEnd
        _updated.Text = dr.GetInt32("UPDATED")
        _updatedBy = dr.GetString("UPDATED_BY").TrimEnd

    End Sub

    Private Sub New()
    End Sub


#End Region ' Factory Methods

#Region "IDoclink"
    Public Function Get_DOL_Reference() As String Implements IDocLink.Get_DOL_Reference
        Return String.Format("{0}#{1}", Get_TransType, _lineNo)
    End Function

    Public Function Get_TransType() As String Implements IDocLink.Get_TransType
        Return Me.GetType.ToClassSchemaName.Leaf
    End Function
#End Region

End Class

