Imports pbs.Helper
Imports System.Data
Imports System.Data.SqlClient
Imports Csla
Imports Csla.Data
Imports Csla.Validation
Imports pbs.BO.DataAnnotations
Imports pbs.BO.Script
Imports pbs.BO.BusinessRules




<Serializable()> _
Public Class CDOC
    Inherits Csla.BusinessBase(Of CDOC)
    Implements Interfaces.IGenPartObject
    Implements IComparable
    Implements IDocLink



#Region "Property Changed"
    Protected Overrides Sub OnDeserialized(context As Runtime.Serialization.StreamingContext)
        MyBase.OnDeserialized(context)
        AddHandler Me.PropertyChanged, AddressOf BO_PropertyChanged
    End Sub

    Private Sub BO_PropertyChanged(sender As Object, e As ComponentModel.PropertyChangedEventArgs) Handles Me.PropertyChanged
        'Select Case e.PropertyName

        '    Case "OrderType"
        '        If Not Me.GetOrderTypeInfo.ManualRef Then
        '            Me._orderNo = POH.AutoReference
        '        End If

        '    Case "OrderDate"
        '        If String.IsNullOrEmpty(Me.OrderPrd) Then Me._orderPrd.Text = Me._orderDate.Date.ToSunPeriod

        '    Case "SuppCode"
        '        For Each line In Lines
        '            line._suppCode = Me.SuppCode
        '        Next

        '    Case "ConvCode"
        '        If String.IsNullOrEmpty(Me.ConvCode) Then
        '            _convRate.Float = 0
        '        Else
        '            Dim conv = pbs.BO.LA.CVInfoList.GetConverter(Me.ConvCode, _orderPrd, String.Empty)
        '            If conv IsNot Nothing Then
        '                _convRate.Float = conv.DefaultRate
        '            End If
        '        End If

        '    Case Else

        'End Select

        pbs.BO.Rules.CalculationRules.Calculator(sender, e)
    End Sub
#End Region

#Region " Business Properties and Methods "
    Private _DTB As String = String.Empty


    Private _lineNo As Integer
    <System.ComponentModel.DataObjectField(True, True)> _
    <CellInfo(Hidden:=True)>
    Public ReadOnly Property LineNo() As String
        Get
            Return _lineNo
        End Get
    End Property

    Private _submitTime As pbs.Helper.SmartDate = New pbs.Helper.SmartDate()
    Public ReadOnly Property SubmitTime() As String
        Get
            Return _submitTime.Text
        End Get
    End Property

    Private _workStation As String = String.Empty
    Public Property WorkStation() As String
        Get
            Return _workStation
        End Get
        Set(ByVal value As String)
            CanWriteProperty("WorkStation", True)
            If value Is Nothing Then value = String.Empty
            If Not _workStation.Equals(value) Then
                _workStation = value
                PropertyHasChanged("WorkStation")
            End If
        End Set
    End Property

    Private _userId As String = String.Empty
    Public Property UserId() As String
        Get
            Return _userId
        End Get
        Set(ByVal value As String)
            CanWriteProperty("UserId", True)
            If value Is Nothing Then value = String.Empty
            If Not _userId.Equals(value) Then
                _userId = value
                PropertyHasChanged("UserId")
            End If
        End Set
    End Property

    Private _localDocUrl As String = String.Empty
    Public Property LocalDocUrl() As String
        Get
            Return _localDocUrl
        End Get
        Set(ByVal value As String)
            CanWriteProperty("LocalDocUrl", True)
            If value Is Nothing Then value = String.Empty
            If Not _localDocUrl.Equals(value) Then
                _localDocUrl = value
                PropertyHasChanged("LocalDocUrl")
            End If
        End Set
    End Property

    Private _cloundDocUrl As String = String.Empty
    Public Property CloundDocUrl() As String
        Get
            Return _cloundDocUrl
        End Get
        Set(ByVal value As String)
            CanWriteProperty("CloundDocUrl", True)
            If value Is Nothing Then value = String.Empty
            If Not _cloundDocUrl.Equals(value) Then
                _cloundDocUrl = value
                PropertyHasChanged("CloundDocUrl")
            End If
        End Set
    End Property

    Private _processingStatus As String = String.Empty
    Public Property ProcessingStatus() As String
        Get
            Return _processingStatus
        End Get
        Set(ByVal value As String)
            CanWriteProperty("ProcessingStatus", True)
            If value Is Nothing Then value = String.Empty
            If Not _processingStatus.Equals(value) Then
                _processingStatus = value
                PropertyHasChanged("ProcessingStatus")
            End If
        End Set
    End Property

    Private _reference As String = String.Empty
    Public Property Reference() As String
        Get
            Return _reference
        End Get
        Set(ByVal value As String)
            CanWriteProperty("Reference", True)
            If value Is Nothing Then value = String.Empty
            If Not _reference.Equals(value) Then
                _reference = value
                PropertyHasChanged("Reference")
            End If
        End Set
    End Property

    Private _descriptn As String = String.Empty
    Public Property Descriptn() As String
        Get
            Return _descriptn
        End Get
        Set(ByVal value As String)
            CanWriteProperty("Description", True)
            If value Is Nothing Then value = String.Empty
            If Not _descriptn.Equals(value) Then
                _descriptn = value
                PropertyHasChanged("Description")
            End If
        End Set
    End Property

    Private _clientCode As String = String.Empty
    Public Property ClientCode() As String
        Get
            Return _clientCode
        End Get
        Set(ByVal value As String)
            CanWriteProperty("ClientCode", True)
            If value Is Nothing Then value = String.Empty
            If Not _clientCode.Equals(value) Then
                _clientCode = value
                PropertyHasChanged("ClientCode")
            End If
        End Set
    End Property

    Private _empCode As String = String.Empty
    Public Property EmpCode() As String
        Get
            Return _empCode
        End Get
        Set(ByVal value As String)
            CanWriteProperty("EmpCode", True)
            If value Is Nothing Then value = String.Empty
            If Not _empCode.Equals(value) Then
                _empCode = value
                PropertyHasChanged("EmpCode")
            End If
        End Set
    End Property

    Private _projectCode As String = String.Empty
    Public Property ProjectCode() As String
        Get
            Return _projectCode
        End Get
        Set(ByVal value As String)
            CanWriteProperty("ProjectCode", True)
            If value Is Nothing Then value = String.Empty
            If Not _projectCode.Equals(value) Then
                _projectCode = value
                PropertyHasChanged("ProjectCode")
            End If
        End Set
    End Property

    Private _docType As String = String.Empty
    Public Property DocType() As String
        Get
            Return _docType
        End Get
        Set(ByVal value As String)
            CanWriteProperty("DocType", True)
            If value Is Nothing Then value = String.Empty
            If Not _docType.Equals(value) Then
                _docType = value
                PropertyHasChanged("DocType")
            End If
        End Set
    End Property

    Private _docDate As pbs.Helper.SmartDate = New pbs.Helper.SmartDate()
    Public Property DocDate() As String
        Get
            Return _docDate.Text
        End Get
        Set(ByVal value As String)
            CanWriteProperty("DocDate", True)
            If value Is Nothing Then value = String.Empty
            If Not _docDate.Equals(value) Then
                _docDate.Text = value
                PropertyHasChanged("DocDate")
            End If
        End Set
    End Property

    Private _contractCode As String = String.Empty
    Public Property ContractCode() As String
        Get
            Return _contractCode
        End Get
        Set(ByVal value As String)
            CanWriteProperty("ContractCode", True)
            If value Is Nothing Then value = String.Empty
            If Not _contractCode.Equals(value) Then
                _contractCode = value
                PropertyHasChanged("ContractCode")
            End If
        End Set
    End Property

    Private _updated As pbs.Helper.SmartDate = New pbs.Helper.SmartDate()
    <CellInfo(Hidden:=True)>
    Public ReadOnly Property Updated() As String
        Get
            Return _updated.Text
        End Get
    End Property

    Private _updatedBy As String = String.Empty
    <CellInfo(Hidden:=True)>
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

#End Region 'Business Properties and Methods

#Region "Validation Rules"

    Private Sub AddSharedCommonRules()
        'Sample simple custom rule
        'ValidationRules.AddRule(AddressOf LDInfo.ContainsValidPeriod, "Period", 1)           

        'Sample dependent property. when check one , check the other as well
        'ValidationRules.AddDependantProperty("AccntCode", "AnalT0")
    End Sub

    Protected Overrides Sub AddBusinessRules()
        AddSharedCommonRules()

        For Each _field As ClassField In ClassSchema(Of CDOC)._fieldList
            If _field.Required Then
                ValidationRules.AddRule(AddressOf Csla.Validation.CommonRules.StringRequired, _field.FieldName, 0)
            End If
            If Not String.IsNullOrEmpty(_field.RegexPattern) Then
                ValidationRules.AddRule(AddressOf Csla.Validation.CommonRules.RegExMatch, New RegExRuleArgs(_field.FieldName, _field.RegexPattern), 1)
            End If
            '----------using lookup, if no user lookup defined, fallback to predefined by developer----------------------------
            If CATMAPInfoList.ContainsCode(_field) Then
                ValidationRules.AddRule(AddressOf LKUInfoList.ContainsLiveCode, _field.FieldName, 2)
                'Else
                '    Select Case _field.FieldName
                '        Case "LocType"
                '            ValidationRules.AddRule(Of LOC, AnalRuleArg)(AddressOf LOOKUPInfoList.ContainsSysCode, New AnalRuleArg(_field.FieldName, SysCats.LocationType))
                '        Case "Status"
                '            ValidationRules.AddRule(Of LOC, AnalRuleArg)(AddressOf LOOKUPInfoList.ContainsSysCode, New AnalRuleArg(_field.FieldName, SysCats.LocationStatus))
                '    End Select
            End If
        Next
        Rules.BusinessRules.RegisterBusinessRules(Me)
        MyBase.AddBusinessRules()
    End Sub
#End Region ' Validation

#Region " Factory Methods "

    Private Sub New()
        _DTB = Context.CurrentBECode
    End Sub

    Public Shared Function BlankCDOC() As CDOC
        Return New CDOC
    End Function

    Public Shared Function NewCDOC(ByVal pLineNo As String) As CDOC
        'If KeyDuplicated(pLineNo) Then ExceptionThower.BusinessRuleStop(String.Format(ResStr(ResStrConst.NOACCESS), ResStr("CDOC")))
        Return DataPortal.Create(Of CDOC)(New Criteria(pLineNo.ToInteger))
    End Function

    Public Shared Function NewBO(ByVal ID As String) As CDOC
        Dim pLineNo As String = ID.Trim

        Return NewCDOC(pLineNo)
    End Function

    Public Shared Function GetCDOC(ByVal pLineNo As String) As CDOC
        Return DataPortal.Fetch(Of CDOC)(New Criteria(pLineNo.ToInteger))
    End Function

    Public Shared Function GetBO(ByVal ID As String) As CDOC
        Dim pLineNo As String = ID.Trim

        Return GetCDOC(pLineNo)
    End Function

    Public Shared Sub DeleteCDOC(ByVal pLineNo As String)
        DataPortal.Delete(New Criteria(pLineNo.ToInteger))
    End Sub

    Public Overrides Function Save() As CDOC
        If Not IsDirty Then ExceptionThower.NotDirty(ResStr(ResStrConst.NOTDIRTY))
        If Not IsSavable Then Throw New Csla.Validation.ValidationException(String.Format(ResStr(ResStrConst.INVALID), ResStr("CDOC")))

        Me.ApplyEdit()
        CDOCInfoList.InvalidateCache()
        Return MyBase.Save()
    End Function

    Public Function CloneCDOC(ByVal pLineNo As String) As CDOC

        'If CDOC.KeyDuplicated(pLineNo) Then ExceptionThower.BusinessRuleStop(ResStr(ResStrConst.CreateAlreadyExists), Me.GetType.ToString.Leaf.Translate)

        Dim cloningCDOC As CDOC = MyBase.Clone
        cloningCDOC._lineNo = 0
        cloningCDOC._DTB = Context.CurrentBECode

        'Todo:Remember to reset status of the new object here 
        cloningCDOC.MarkNew()
        cloningCDOC.ApplyEdit()

        cloningCDOC.ValidationRules.CheckRules()

        Return cloningCDOC
    End Function

#End Region ' Factory Methods

#Region " Data Access "

    <Serializable()> _
    Private Class Criteria
        Public _lineNo As Integer

        Public Sub New(ByVal pLineNo As String)
            _lineNo = pLineNo.ToInteger

        End Sub
    End Class

    <RunLocal()> _
    Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        _lineNo = criteria._lineNo

        ValidationRules.CheckRules()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Using ctx = ConnectionManager.GetManager
            Using cm = ctx.Connection.CreateCommand()
                cm.CommandType = CommandType.Text
                cm.CommandText = <SqlText>SELECT * FROM pbs_CLIENT_DOC WHERE LINE_NO= <%= criteria._lineNo %></SqlText>.Value.Trim

                Using dr As New SafeDataReader(cm.ExecuteReader)
                    If dr.Read Then
                        FetchObject(dr)
                        MarkOld()
                    End If
                End Using

            End Using
        End Using
    End Sub

    Private Sub FetchObject(ByVal dr As SafeDataReader)
        _lineNo = dr.GetInt32("LINE_NO")
        _submitTime.Text = dr.GetDateTime("SUBMIT_TIME")
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

    Private Shared _lockObj As New Object
    Protected Overrides Sub DataPortal_Insert()
        SyncLock _lockObj
            Using ctx = ConnectionManager.GetManager
                Using cm = ctx.Connection.CreateCommand()

                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "pbs_CLIENT_DOC_Insert"

                    cm.Parameters.AddWithValue("@LINE_NO", _lineNo).Direction = ParameterDirection.Output
                    AddInsertParameters(cm)
                    cm.ExecuteNonQuery()

                End Using
            End Using
        End SyncLock
    End Sub

    Private Sub AddInsertParameters(ByVal cm As SqlCommand)

        cm.Parameters.AddWithValue("@SUBMIT_TIME", Now)
        cm.Parameters.AddWithValue("@WORK_STATION", _workStation.Trim)
        cm.Parameters.AddWithValue("@USER_ID", _userId.Trim)
        cm.Parameters.AddWithValue("@LOCAL_DOC_URL", _localDocUrl.Trim)
        cm.Parameters.AddWithValue("@CLOUND_DOC_URL", _cloundDocUrl.Trim)
        cm.Parameters.AddWithValue("@PROCESSING_STATUS", _processingStatus.Trim)
        cm.Parameters.AddWithValue("@REFERENCE", _reference.Trim)
        cm.Parameters.AddWithValue("@DESCRIPTION", _descriptn.Trim)
        cm.Parameters.AddWithValue("@CLIENT_CODE", _clientCode.Trim)
        cm.Parameters.AddWithValue("@EMP_CODE", _empCode.Trim)
        cm.Parameters.AddWithValue("@PROJECT_CODE", _projectCode.Trim)
        cm.Parameters.AddWithValue("@DOC_TYPE", _docType.Trim)
        cm.Parameters.AddWithValue("@DOC_DATE", _docDate.DBValue)
        cm.Parameters.AddWithValue("@CONTRACT_CODE", _contractCode.Trim)
        cm.Parameters.AddWithValue("@UPDATED", ToDay.ToSunDate)
        cm.Parameters.AddWithValue("@UPDATED_BY", Context.CurrentUserCode)
    End Sub


    Protected Overrides Sub DataPortal_Update()
        SyncLock _lockObj
            Using ctx = ConnectionManager.GetManager
                Using cm = ctx.Connection.CreateCommand()

                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "pbs_CLIENT_DOC_Update"

                    cm.Parameters.AddWithValue("@LINE_NO", _lineNo)
                    AddInsertParameters(cm)
                    cm.ExecuteNonQuery()

                End Using
            End Using
        End SyncLock
    End Sub

    Protected Overrides Sub DataPortal_DeleteSelf()
        DataPortal_Delete(New Criteria(_lineNo))
    End Sub

    Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
        Using ctx = ConnectionManager.GetManager
            Using cm = ctx.Connection.CreateCommand()

                cm.CommandType = CommandType.Text
                cm.CommandText = <SqlText>DELETE pbs_CLIENT_DOC WHERE LINE_NO= <%= criteria._lineNo %></SqlText>.Value.Trim
                cm.ExecuteNonQuery()

            End Using
        End Using

    End Sub

    'Protected Overrides Sub DataPortal_OnDataPortalInvokeComplete(ByVal e As Csla.DataPortalEventArgs)
    '    If Csla.ApplicationContext.ExecutionLocation = ExecutionLocations.Server Then
    '        CDOCInfoList.InvalidateCache()
    '    End If
    'End Sub


#End Region 'Data Access                           

#Region " Exists "
    Public Shared Function Exists(ByVal pLineNo As String) As Boolean
        Return CDOCInfoList.ContainsCode(pLineNo)
    End Function

    '    Public Shared Function KeyDuplicated(ByVal pLineNo As SmartInt32) As Boolean
    '        Dim SqlText = <SqlText>SELECT COUNT(*) FROM pbs_CLIENT_DOC WHERE DTB='<%= Context.CurrentBECode %>'  AND LINE_NO= '<%= pLineNo %>'</SqlText>.Value.Trim
    '        Return SQLCommander.GetScalarInteger(SqlText) > 0
    '    End Function
#End Region

#Region " IGenpart "

    Public Function CloneBO(ByVal id As String) As Object Implements Interfaces.IGenPartObject.CloneBO
        Return CloneCDOC(id)
    End Function

    Public Function getBO1(ByVal id As String) As Object Implements Interfaces.IGenPartObject.GetBO
        Return GetBO(id)
    End Function

    Public Function myCommands() As String() Implements Interfaces.IGenPartObject.myCommands
        Return pbs.Helper.Action.StandardReferenceCommands
    End Function

    Public Function myFullName() As String Implements Interfaces.IGenPartObject.myFullName
        Return GetType(CDOC).ToString
    End Function

    Public Function myName() As String Implements Interfaces.IGenPartObject.myName
        Return GetType(CDOC).ToString.Leaf
    End Function

    Public Function myQueryList() As IList Implements Interfaces.IGenPartObject.myQueryList
        Return CDOCInfoList.GetCDOCInfoList
    End Function
#End Region

#Region "IDoclink"
    Public Function Get_DOL_Reference() As String Implements IDocLink.Get_DOL_Reference
        Return String.Format("{0}#{1}", Get_TransType, _lineNo)
    End Function

    Public Function Get_TransType() As String Implements IDocLink.Get_TransType
        Return Me.GetType.ToClassSchemaName.Leaf
    End Function
#End Region

End Class

