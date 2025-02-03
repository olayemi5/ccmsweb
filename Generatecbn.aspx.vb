Imports CCMS.BusinessObject
Imports CCMS.DataAccess
Imports System.Data.SqlClient
Imports CCMS.BusinessLogic
Imports System.Xml
Public Class Generatecbn
    Inherits System.Web.UI.Page

  

  

    Protected Sub Button1_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles btnxmlfile.Click
        loadfileXML()
    End Sub

    
    Sub loadfileXML()
        Try
            Dim lstloadfile As LoadfileList2
            Dim recloadfile As loadfile2
            Dim vtracknum1 As String = Nothing
            lstloadfile = loadfile2DB.getloadfile2()

            'creating  datatable good records
            Dim rdata2 As New DataTable()
            rdata2.Columns.Add(New DataColumn("tracknum", GetType(String)))
            rdata2.Columns.Add(New DataColumn("branch_name", GetType(String)))
            rdata2.Columns.Add(New DataColumn("technician", GetType(String)))
            rdata2.Columns.Add(New DataColumn("client_type", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_name", GetType(String)))
            rdata2.Columns.Add(New DataColumn("First_name_pet", GetType(String)))
            rdata2.Columns.Add(New DataColumn("Middle_name_pet", GetType(String)))
            rdata2.Columns.Add(New DataColumn("Last_name_pet", GetType(String)))
            rdata2.Columns.Add(New DataColumn("acct_num", GetType(String)))
            rdata2.Columns.Add(New DataColumn("acct_type", GetType(String)))
            rdata2.Columns.Add(New DataColumn("acct_ccy", GetType(String)))
            rdata2.Columns.Add(New DataColumn("addr_1_comp", GetType(String)))
            rdata2.Columns.Add(New DataColumn("addr_2_comp", GetType(String)))
            rdata2.Columns.Add(New DataColumn("acct_owner_city", GetType(String)))
            rdata2.Columns.Add(New DataColumn("acct_owner_state", GetType(String)))
            rdata2.Columns.Add(New DataColumn("acct_owner_country", GetType(String)))
            rdata2.Columns.Add(New DataColumn("acct_owner_pcode", GetType(String)))
            rdata2.Columns.Add(New DataColumn("acct_owner_phnum", GetType(String)))
            rdata2.Columns.Add(New DataColumn("acct_owner_offphnum", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_channel", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_location", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_email", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_fininmpl", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_cat", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_subcat", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_subj", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_desc", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_prayer", GetType(String)))
            rdata2.Columns.Add(New DataColumn("comp_date_recv", GetType(DateTime)))
            rdata2.Columns.Add(New DataColumn("comp_date_clsed", GetType(DateTime)))
            rdata2.Columns.Add(New DataColumn("amt_applicable", GetType(Double)))
            rdata2.Columns.Add(New DataColumn("amt_refunde", GetType(Double)))
            rdata2.Columns.Add(New DataColumn("amt_recovd", GetType(Double)))
            rdata2.Columns.Add(New DataColumn("action_taken", GetType(String)))
            rdata2.Columns.Add(New DataColumn("Status", GetType(String)))
            rdata2.Columns.Add(New DataColumn("bank_remark", GetType(String)))
            rdata2.Columns.Add(New DataColumn("Root_cause", GetType(String)))
            rdata2.Columns.Add(New DataColumn("Preferred_contact_phone", GetType(String)))
            rdata2.Columns.Add(New DataColumn("Preferred_contact_Email", GetType(String)))
            rdata2.Columns.Add(New DataColumn("Preferred_contact_address", GetType(String)))
            rdata2.Columns.Add(New DataColumn("UniqueIdentificationNumber", GetType(String)))






            'creating  datatable records
            Dim rdata3 As New DataTable()
            rdata3.Columns.Add(New DataColumn("tracknum", GetType(String)))
            rdata3.Columns.Add(New DataColumn("branch_name", GetType(String)))
            rdata3.Columns.Add(New DataColumn("technician", GetType(String)))
            rdata3.Columns.Add(New DataColumn("client_type", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_name", GetType(String)))
            rdata3.Columns.Add(New DataColumn("First_name_pet", GetType(String)))
            rdata3.Columns.Add(New DataColumn("Middle_name_pet", GetType(String)))
            rdata3.Columns.Add(New DataColumn("Last_name_pet", GetType(String)))
            rdata3.Columns.Add(New DataColumn("acct_num", GetType(String)))
            rdata3.Columns.Add(New DataColumn("acct_type", GetType(String)))
            rdata3.Columns.Add(New DataColumn("acct_ccy", GetType(String)))
            rdata3.Columns.Add(New DataColumn("addr_1_comp", GetType(String)))
            rdata3.Columns.Add(New DataColumn("addr_2_comp", GetType(String)))
            rdata3.Columns.Add(New DataColumn("acct_owner_city", GetType(String)))
            rdata3.Columns.Add(New DataColumn("acct_owner_state", GetType(String)))
            rdata3.Columns.Add(New DataColumn("acct_owner_country", GetType(String)))
            rdata3.Columns.Add(New DataColumn("acct_owner_pcode", GetType(String)))
            rdata3.Columns.Add(New DataColumn("acct_owner_phnum", GetType(String)))
            rdata3.Columns.Add(New DataColumn("acct_owner_offphnum", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_channel", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_location", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_email", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_fininmpl", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_cat", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_subcat", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_subj", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_desc", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_prayer", GetType(String)))
            rdata3.Columns.Add(New DataColumn("comp_date_recv", GetType(DateTime)))
            rdata3.Columns.Add(New DataColumn("comp_date_clsed", GetType(DateTime)))
            rdata3.Columns.Add(New DataColumn("amt_applicable", GetType(Double)))
            rdata3.Columns.Add(New DataColumn("amt_refunde", GetType(Double)))
            rdata3.Columns.Add(New DataColumn("amt_recovd", GetType(Double)))
            rdata3.Columns.Add(New DataColumn("action_taken", GetType(String)))
            rdata3.Columns.Add(New DataColumn("Status", GetType(String)))
            rdata3.Columns.Add(New DataColumn("bank_remark", GetType(String)))
            rdata3.Columns.Add(New DataColumn("Root_cause", GetType(String)))
            rdata3.Columns.Add(New DataColumn("Preferred_contact_phone", GetType(String)))
            rdata3.Columns.Add(New DataColumn("Preferred_contact_Email", GetType(String)))
            rdata3.Columns.Add(New DataColumn("Preferred_contact_address", GetType(String)))
            rdata3.Columns.Add(New DataColumn("UniqueIdentificationNumber", GetType(String)))



            For Each recloadfile In lstloadfile


                Dim vnamesspilt() As String
                Dim vtracknum As String = ""
                Dim vbranch_name As String = ""
                Dim vtechnician As String = ""
                Dim vclient_type As String = ""
                Dim vvclient_type As String = ""
                Dim vcomp_name As String = ""
                Dim vFirst_name_pet As String = ""
                Dim vMiddle_name_pet As String = ""
                Dim vLast_name_pet As String = ""
                Dim vacct_num As String = ""
                Dim vacct_type As String = ""
                Dim vvacct_type As String = ""
                Dim vacct_ccy As String = ""
                Dim vaddr_1_comp As String = ""
                Dim vaddr_2_comp As String = ""
                Dim vacct_owner_city As String = ""
                Dim vacct_owner_state As String = ""
                Dim vacct_owner_country As String = ""
                Dim vacct_owner_pcode As String = ""
                Dim vacct_owner_phnum As String = ""
                Dim vacct_owner_offphnum As String = ""
                Dim vcomp_channel As String = ""
                Dim vcomp_location As String = ""
                Dim vtemp_comp_location As String = ""
                Dim vcomp_email As String = ""
                Dim vcomp_fininmpl As String = ""
                Dim vcomp_cat As String = ""
                Dim vcomp_subcat As String = ""
                Dim vcomp_subj As String = ""
                Dim vcomp_desc As String = ""
                Dim vcomp_item As String = ""
                Dim vcomp_prayer As String = ""
                Dim vcomp_date_recv As String = Nothing
                Dim vcomp_date_clsed As String = Nothing
                Dim vamt_applicable As Double = 0
                Dim vamt_refunde As Double = 0
                Dim vamt_recovd As Double = 0
                Dim vaction_taken As String = ""
                Dim vStatus As String = ""
                Dim vbank_remark As String = ""
                Dim vacid As String = ""
                Dim vcustid As String = ""
                Dim vcomp_namegam As String = ""
                Dim vRoot_cause As String = ""
                Dim vtempRoot_cause As String = ""
                Dim vPreferred_contact_phone As String = ""
                Dim vPreferred_contact_Email As String = ""
                Dim vPreferred_contact_address As String = ""
                Dim vUniqueIdentificationNumber As String = ""


                vtracknum = recloadfile.Requestid
                vacct_num = recloadfile.AccountNumber
                vcomp_channel = recloadfile.OriginatedBy
                vcomp_name = recloadfile.CustomerName.ToUpper
                vcomp_subcat = recloadfile.SubCategory
                vcomp_subj = recloadfile.Subject
                vtemp_comp_location = recloadfile.OriginatedBy
                vcomp_item = recloadfile.Item
                vbranch_name = recloadfile.Branch
                vcomp_date_clsed = recloadfile.CompletedTime
                vcomp_cat = recloadfile.comp_cat_code
                vcomp_subcat = recloadfile.comp_subcat_code
                vtempRoot_cause = recloadfile.root_cause




                vamt_applicable = recloadfile.Amount
                vcomp_desc = recloadfile.Item
                vtechnician = recloadfile.Technician
                vcomp_date_recv = recloadfile.CreatedTime

                vStatus = recloadfile.RequestStatus
                vbank_remark = ""

                vcomp_email = recloadfile.EmailAddress
                vPreferred_contact_Email = recloadfile.EmailAddress




                Dim ogaminfoList As New gaminfoList
                Dim ogaminfo As New gaminfo
                ogaminfo.foracid = vacct_num

                Dim ocmginfo As New cmginfo
                Dim ocmginfoList As New cmginfoList



                ogaminfoList = gamInfoManager.getGamInfobyForacid(ogaminfo)


                For Each ogaminfo In ogaminfoList
                    vbranch_name = ogaminfo.sol_id
                    vacct_ccy = ogaminfo.acct_ccy
                    vcustid = ogaminfo.custid
                    vacid = ogaminfo.acct_acid
                    vacct_type = ogaminfo.schm_type
                    vcomp_namegam = ogaminfo.acct_name

                Next



                ocmginfo.custid = vcustid
                ocmginfoList = cmgInfoManager.getCmgInfobyCustId2(ocmginfo)
                For Each ocmginfo In ocmginfoList
                    vaddr_1_comp = ocmginfo.cust_addr1
                    vaddr_2_comp = ocmginfo.cust_addr2
                    vacct_owner_city = ocmginfo.cust_comu_city_code
                    vacct_owner_state = ocmginfo.cust_state
                    vacct_owner_country = ocmginfo.cust_ctrycode
                    vacct_owner_pcode = ocmginfo.cust_comu_pin_code
                    vacct_owner_phnum = ocmginfo.cust_comu_phone_num_1

                    vPreferred_contact_phone = ocmginfo.cust_phone1
                    vPreferred_contact_address = vaddr_1_comp & "," & vaddr_2_comp

                    vacct_owner_offphnum = ocmginfo.cust_comu_phone_num_2
                    vclient_type = ocmginfo.free_code_7
                    vUniqueIdentificationNumber = ocmginfo.UniqueIdentificationNumber
                    Dim gbranch As New branch
                    gbranch.sol_id = ocmginfo.cust_branchCode


                    vcomp_location = branchManager.branchbysolid(gbranch)
                    If vcomp_item.ToUpper Like "%NOT ON US" Or vcomp_item.ToUpper Like "%OTHER BANK%" Or vcomp_item.ToUpper Like "%LEFT THE BANK%" Then
                        vbranch_name = "_000_"
                    Else
                        vbranch_name = branchManager.branchbysolid(gbranch)
                        vaction_taken = ""
                    End If
                    If vbranch_name Is Nothing Or vbranch_name = "" Then
                        vbranch_name = "HEAD OFFICE"
                    End If

                    vamt_refunde = 0
                    vamt_recovd = 0
                Next



                If UCase(vcomp_channel) = "CUSTOMER" Then
                    vcomp_channel = "EMAIL"
                    vcomp_location = ""
                End If
                If UCase(vcomp_channel) = "ACCOUNT OFFICER" Then
                    vcomp_channel = "WALK_IN"
                End If
                If vcomp_location Is Nothing Or vcomp_location = "" Then
                    vcomp_location = vcomp_channel
                End If



                vcomp_email = ocmginfo.cust_email

                If IsNumeric(vamt_applicable) Then
                    vcomp_fininmpl = "Y"
                Else
                    vcomp_fininmpl = "N"
                End If
                Select Case vclient_type
                    Case "INDV"
                        vvclient_type = "INDV"
                    Case "CORP"
                        vvclient_type = "CORP"
                    Case Else
                        vvclient_type = "INDV"
                End Select

                Select Case vacct_type
                    Case "CAA"
                        vvacct_type = "CURRENT"
                    Case "LAA"
                        vvacct_type = "LOAN"
                    Case "ODA"
                        vvacct_type = "CURRENT"
                    Case "SBA"
                        vvacct_type = "SAVINGS"
                    Case "TDA"
                        vvacct_type = "CURRENT"
                    Case Else
                        vvacct_type = "CURRENT"
                End Select


                If vclient_type = "CORP" Then

                    If (vcomp_namegam).Length <= 0 Then
                        vFirst_name_pet = vcomp_name
                        vLast_name_pet = vcomp_name
                        vMiddle_name_pet = "N/A"

                    Else
                        vcomp_name = vcomp_namegam
                        vFirst_name_pet = vcomp_namegam
                        vLast_name_pet = vcomp_namegam
                        vMiddle_name_pet = "N/A"
                    End If
                Else
                    If (Trim(vcomp_namegam)).Length <= 0 Then
                        vnamesspilt = Trim(vcomp_name).Split(CChar(" "))
                        vFirst_name_pet = vnamesspilt(0)

                        If vnamesspilt.Length >= 3 Then
                            vMiddle_name_pet = vnamesspilt(1)
                            vLast_name_pet = vnamesspilt(2)
                        End If

                        If vnamesspilt.Length = 2 Then
                            vLast_name_pet = vnamesspilt(1)
                            vMiddle_name_pet = "N/A"
                        End If
                        If vnamesspilt.Length = 1 Then
                            vLast_name_pet = "N/A"
                            vMiddle_name_pet = "N/A"
                        End If
                    Else
                        vcomp_name = vcomp_namegam
                        vnamesspilt = Trim(vcomp_namegam).Split(CChar(" "))
                        vFirst_name_pet = vnamesspilt(0)

                        If vnamesspilt.Length >= 3 Then
                            vMiddle_name_pet = vnamesspilt(1)
                            vLast_name_pet = vnamesspilt(2)
                        End If

                        If vnamesspilt.Length = 2 Then
                            vLast_name_pet = vnamesspilt(1)
                            vMiddle_name_pet = "N/A"
                        End If
                        If vnamesspilt.Length = 1 Then
                            vLast_name_pet = "N/A"
                            vMiddle_name_pet = "N/A"
                        End If

                    End If

                End If
                If vLast_name_pet = "N/A" Or vLast_name_pet Is Nothing Then
                    vLast_name_pet = vFirst_name_pet
                End If
                If vacct_owner_city Is Nothing Then
                    vacct_owner_city = "NIL"
                End If



                Dim otestaccttype As New accttype
                Dim otestactntaken As New actntaken
                Dim otestcurrencys As New currencys
                Dim otestcountry As New country
                Dim oteststate As New states



                otestaccttype.account_type_code = vvacct_type
                otestactntaken.actn_tkn_code = vaction_taken
                otestcurrencys.ccy_code = vacct_ccy

                otestcountry.country_code = vacct_owner_country
                oteststate.state_code = vacct_owner_state

                'Dim vaccttypetest As Boolean = AcctTypeMaanager.validateAcctType(otestaccttype)
                'Dim vactntakentest As Boolean = ActnTakenManager.retrActnTaken2(otestactntaken)
                ' Dim vcurrencytest As Boolean = currencyManager.retrCurrencybyCode2(otestcurrencys)
                'Dim vcountrytest As Boolean = countryManager.retrCountry2(otestcountry)
                'Dim vstatetest As Boolean = stateManager.retriveState(oteststate)


                If vacct_ccy = "USD" Or vacct_ccy = "GBP" Or vacct_ccy = "EUR" Or vacct_ccy = "ATS" Or vacct_ccy = "BEF" Or vacct_ccy = "CAD" Or vacct_ccy = "CHF" Or vacct_ccy = "DEM" Or vacct_ccy = "DKK" Or vacct_ccy = "ESP" Or vacct_ccy = "FRF" Or vacct_ccy = "ITL" Or vacct_ccy = "JPY" Or vacct_ccy = "NLG" Then
                    vvacct_type = "DOMICILIARY"
                Else
                    vacct_ccy = "NGN"
                End If



                'use default value where no data is supplied

                If Trim(vtracknum).Length <= 0 Then
                    vtracknum = "000000"
                End If

                'If Trim(vcomp_location) Is Nothing Then
                '    vcomp_location = "FCMB PLC"
                'End If

                If Trim(vclient_type).Length <= 0 Then
                    vclient_type = "OTHERS"
                End If
                'If Trim(vcomp_name) Is Nothing Then
                '    vcomp_name = "N/A"
                'End If
                If Trim(vFirst_name_pet) Is Nothing Or Len(Trim(vFirst_name_pet)) = 0 Then
                    vFirst_name_pet = "N/A"
                End If
                If Trim(vMiddle_name_pet) Is Nothing Or Len(Trim(vMiddle_name_pet)) = 0 Then
                    vMiddle_name_pet = "N/A"
                End If
                If Trim(vLast_name_pet) Is Nothing Or Len(Trim(vLast_name_pet)) = 0 Then
                    vLast_name_pet = "N/A"
                End If
                If Trim(vaddr_1_comp) Is Nothing Or Trim(vaddr_1_comp) = "" Then
                    vaddr_1_comp = "CITY"
                End If
                If Trim(vaddr_2_comp) Is Nothing Or Trim(vaddr_2_comp) = "" Then
                    vaddr_2_comp = vaddr_1_comp
                End If
                If Trim(vacct_owner_city) Is Nothing Or vacct_owner_city = "" Then
                    vacct_owner_city = "CITY"
                End If
                If Trim(vcomp_subcat) Is Nothing Or Trim(vcomp_subcat).Length = 0 Then
                    vcomp_subcat = "A999"
                    vcomp_cat = "A"
                End If
                If Trim(vcomp_subj) Is Nothing Then
                    vcomp_subj = "N/A"
                End If
                If Trim(vcomp_desc) Is Nothing Then
                    vcomp_desc = "USE SUBJECT"
                End If
                'If CStr(vcomp_date_recv) Is Nothing Then
                '    vcomp_date_recv = Today
                'End If
                If Trim(vaction_taken) Is Nothing Then
                    vaction_taken = "WIP/ RESOLVED"
                End If
                If Trim(vacct_num) Is Nothing Or vacct_num = "" Then
                    vacct_num = "0000000000"
                End If
                If Trim(vacct_owner_state) Is Nothing Or Len(vacct_owner_state) = 0 Then
                    vacct_owner_state = "N/A"
                End If
                If Trim(vacct_owner_country) Is Nothing Or Len(vacct_owner_country) = 0 Or vacct_owner_country = "NG" Then
                    vacct_owner_country = "NGA"
                End If
                If Trim(vacct_owner_pcode) Is Nothing Or Trim(vacct_owner_pcode).Length = 0 Or vacct_owner_pcode = "0" Or vacct_owner_pcode = "." Then
                    vacct_owner_pcode = "234"
                End If

                If Trim(vtechnician) Is Nothing Then
                    vtechnician = "N/A"
                End If

                If vStatus.ToUpper = "CLOSED" Or vStatus.ToUpper = "RESOLVED" Then
                    vStatus = "CLOSED"
                    vamt_refunde = vamt_applicable
                    vamt_recovd = vamt_applicable
                ElseIf vStatus.ToUpper = "OPEN" Then
                    vStatus = "OPEN"
                Else
                    vStatus = "PENDING"
                End If

                If vStatus = "CLOSED" Then
                    vRoot_cause = vtempRoot_cause
                    vamt_refunde = vamt_applicable
                    vamt_recovd = vamt_applicable
                Else
                    vRoot_cause = "N/A"
                End If
                If vStatus = "PENDING_INPUT" Then
                    vStatus = "PENDING"
                End If
                If vacct_owner_phnum Is Nothing Or vacct_owner_phnum = "" Or vacct_owner_phnum = "N/A" Then
                    vacct_owner_phnum = "00000000000"
                End If


                If vacct_owner_offphnum Is Nothing Or vacct_owner_offphnum = "" Then
                    vacct_owner_offphnum = vacct_owner_phnum
                End If

                If Trim(vcomp_prayer) Is Nothing Or vcomp_prayer = "" Then
                    vcomp_prayer = "Customers want their issues resolved"
                End If

                If vPreferred_contact_phone Is Nothing Or vPreferred_contact_phone = "" Then
                    vPreferred_contact_phone = "N/A"
                End If
                If vPreferred_contact_address Is Nothing Or vPreferred_contact_address = "" Then
                    vPreferred_contact_address = "N/A"
                End If
                If vbranch_name Is Nothing Or vbranch_name = "" Then
                    vbranch_name = "HEAD OFFICE"
                End If

                If vcomp_email Is Nothing Or vcomp_email = "" Then
                    vcomp_email = "N/A"
                End If
                'If vcomp_date_recv > vcomp_date_clsed Then
                '    vcomp_date_clsed = vcomp_date_recv
                'End If


                If vcomp_channel = "WALK-IN" Then
                    vcomp_channel = "WALK_IN"
                End If
                If vcomp_channel = "Contact centre" Then
                    vcomp_channel = "PHONE"
                End If
                If vcomp_channel = "Support Staff" Then
                    vcomp_channel = "OTHERS"
                End If
                If vcomp_channel = "Customer Solution" Then
                    vcomp_channel = "EMAIL"
                End If
                'set values to insert into table

                If Trim(vtracknum) <> Trim(vtracknum1) Then
                    Dim dr As DataRow = rdata2.NewRow()
                    dr("tracknum") = Mid(LTrim(RTrim(vtracknum)), 1, 50)
                    dr("branch_name") = vbranch_name
                    'dr("technician") = Mid(LTrim(RTrim(vtechnician)), 1, 100)
                    dr("technician") = "N/A"
                    dr("client_type") = vvclient_type
                    dr("comp_name") = Mid(LTrim(RTrim(vcomp_name)), 1, 250)
                    dr("First_name_pet") = Mid(LTrim(RTrim(vFirst_name_pet)), 1, 100)
                    dr("Middle_name_pet") = Mid(LTrim(RTrim(vMiddle_name_pet)), 1, 100)
                    dr("Last_name_pet") = Mid(LTrim(RTrim(vLast_name_pet)), 1, 100)
                    dr("acct_num") = Mid(LTrim(RTrim(vacct_num)), 1, 10)
                    dr("acct_type") = vvacct_type
                    dr("acct_ccy") = vacct_ccy
                    dr("addr_1_comp") = Mid(LTrim(RTrim(vaddr_1_comp)), 1, 255)
                    dr("addr_2_comp") = Mid(LTrim(RTrim(vaddr_2_comp)), 1, 255)
                    dr("acct_owner_city") = Mid(LTrim(RTrim(vacct_owner_city)), 1, 50)
                    dr("acct_owner_state") = Mid(LTrim(RTrim(vacct_owner_state)), 1, 50)
                    dr("acct_owner_country") = Mid(LTrim(RTrim(vacct_owner_country)), 1, 50)
                    dr("acct_owner_pcode") = Mid(LTrim(RTrim(vacct_owner_pcode)), 1, 50)
                    dr("acct_owner_phnum") = Mid(LTrim(RTrim(vacct_owner_phnum)), 1, 20)
                    dr("acct_owner_offphnum") = Mid(LTrim(RTrim(vacct_owner_offphnum)), 1, 20)
                    dr("comp_channel") = Mid(LTrim(RTrim(vcomp_channel)), 1, 20)
                    dr("comp_location") = vcomp_location
                    dr("comp_email") = vcomp_email
                    dr("comp_fininmpl") = vcomp_fininmpl
                    dr("comp_subj") = vcomp_item
                    dr("comp_desc") = vcomp_desc
                    dr("comp_cat") = vcomp_cat
                    dr("comp_subcat") = vcomp_subcat
                    dr("comp_prayer") = vcomp_prayer
                    dr("comp_date_recv") = vcomp_date_recv
                    dr("comp_date_clsed") = vcomp_date_clsed
                    dr("amt_applicable") = vamt_applicable
                    dr("amt_refunde") = vamt_refunde
                    dr("amt_recovd") = vamt_recovd
                    dr("action_taken") = vaction_taken
                    dr("Status") = vStatus
                    dr("bank_remark") = vbank_remark
                    dr("Root_cause") = vRoot_cause
                    dr("Preferred_contact_phone") = vPreferred_contact_phone
                    dr("Preferred_contact_Email") = vPreferred_contact_Email
                    dr("Preferred_contact_address") = vPreferred_contact_address
                    dr("UniqueIdentificationNumber") = vUniqueIdentificationNumber
                    rdata2.Rows.Add(dr)
                    vtracknum1 = vtracknum
                End If

                '
            Next

            'lstloadfile = loadfile2DB.getloadfile2()
            'For Each recloadfile In lstloadfile

            'Next

            Try
                Dim sbs As New SqlBulkCopy(CCMS.DataAccess.AppConfig.CCMSDB)
                sbs.DestinationTableName = "[dbo].[ccms_comp_detail2]"
                sbs.ColumnMappings.Add("tracknum", "tracknum")
                sbs.ColumnMappings.Add("branch_name", "branch_name")
                sbs.ColumnMappings.Add("technician", "technician")
                sbs.ColumnMappings.Add("client_type", "client_type")
                sbs.ColumnMappings.Add("comp_name", "comp_name")
                sbs.ColumnMappings.Add("First_name_pet", "First_name_pet")
                sbs.ColumnMappings.Add("Middle_name_pet", "Middle_name_pet")
                sbs.ColumnMappings.Add("Last_name_pet", "Last_name_pet")
                sbs.ColumnMappings.Add("acct_num", "acct_num")
                sbs.ColumnMappings.Add("acct_type", "acct_type")
                sbs.ColumnMappings.Add("acct_ccy", "acct_ccy")
                sbs.ColumnMappings.Add("addr_1_comp", "addr_1_comp")
                sbs.ColumnMappings.Add("addr_2_comp", "addr_2_comp")
                sbs.ColumnMappings.Add("acct_owner_city", "acct_owner_city")
                sbs.ColumnMappings.Add("acct_owner_state", "acct_owner_state")
                sbs.ColumnMappings.Add("acct_owner_country", "acct_owner_country")
                sbs.ColumnMappings.Add("acct_owner_pcode", "acct_owner_pcode")
                sbs.ColumnMappings.Add("acct_owner_phnum", "acct_owner_phnum")
                sbs.ColumnMappings.Add("acct_owner_offphnum", "acct_owner_offphnum")
                sbs.ColumnMappings.Add("comp_channel", "comp_channel")
                sbs.ColumnMappings.Add("comp_location", "comp_location")
                sbs.ColumnMappings.Add("comp_email", "comp_email")
                sbs.ColumnMappings.Add("comp_fininmpl", "comp_fininmpl")
                sbs.ColumnMappings.Add("comp_cat", "comp_cat")
                sbs.ColumnMappings.Add("comp_prayer", "comp_prayer")
                sbs.ColumnMappings.Add("comp_date_recv", "comp_date_recv")
                sbs.ColumnMappings.Add("comp_date_clsed", "comp_date_clsed")
                sbs.ColumnMappings.Add("amt_applicable", "amt_applicable")
                sbs.ColumnMappings.Add("amt_refunde", "amt_refunde")
                sbs.ColumnMappings.Add("amt_recovd", "amt_recovd")
                sbs.ColumnMappings.Add("comp_subj", "comp_subj")
                sbs.ColumnMappings.Add("comp_subcat", "comp_subcat")
                sbs.ColumnMappings.Add("comp_desc", "comp_desc")
                sbs.ColumnMappings.Add("action_taken", "action_taken")
                sbs.ColumnMappings.Add("Status", "Status")
                sbs.ColumnMappings.Add("bank_remark", "bank_remark")
                sbs.ColumnMappings.Add("Root_cause", "Root_cause")
                sbs.ColumnMappings.Add("Preferred_contact_phone", "Preferred_contact_phone")
                sbs.ColumnMappings.Add("Preferred_contact_Email", "Preferred_contact_Email")
                sbs.ColumnMappings.Add("Preferred_contact_address", "Preferred_contact_address")
                sbs.ColumnMappings.Add("UniqueIdentificationNumber", "UniqueIdentificationNumber")
                sbs.WriteToServer(rdata2)

                'Using writer As XmlWriter = XmlWriter.Create("C:\Test.xml", settings)
                '    writer
                '    Dim filePath = "C:\Test.xml"
                '    lstloadfile'.WriteXml(filePath)
                '    rdata2.WriteXml(Server.MapPath("XMLFiles/MyXMLFile.xml"))
                'End Using
                sbs.Close()
            Catch ex As Exception
        
           ClientScript.RegisterStartupScript(GetType(String), "myalert", "alert('" + ex.Message.ToString + "' );", True)
            End Try





            ClientScript.RegisterStartupScript(GetType(String), "myalert", "alert('" + "XML File Processed Successfully" + "' );", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(GetType(String), "myalert", "alert('" + ex.Message.ToString + "' );", True)
        End Try
    End Sub

    Protected Sub btnclrxml_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnclrxml.Click

        Dim result As Integer = Complainmanager2.cleanCompDetail()
        If result > 0 Then
            ClientScript.RegisterStartupScript(GetType(String), "myalert", "alert('" + "Records Deleted successfully" + "' );", True)
        Else
            ClientScript.RegisterStartupScript(GetType(String), "myalert", "alert('" + "Records could not be deleted" + "' );", True)
        End If

    End Sub

End Class