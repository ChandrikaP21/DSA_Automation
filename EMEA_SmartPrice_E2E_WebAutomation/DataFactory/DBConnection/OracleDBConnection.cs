using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;

using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Compare;
using Guidance = EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Guidance;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;

namespace EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection
{
    public class OracleDBConnection
    {
        public readonly static string oradb = string.Empty;
        public readonly static string oradb1 = string.Empty;
        public readonly static string g4Oradb = string.Empty;
        public readonly static string g2Oradb = string.Empty;
        public readonly static string ProdOradb = string.Empty;
        public readonly static string ReportProdOradb = string.Empty;


        //OracleConnection con = new OracleConnection(ReportProdOradb);
        OracleConnection con = new OracleConnection(ReportProdOradb);
        OracleCommand cmd = new OracleCommand();
        OracleCommand retriveSPCmd = new OracleCommand();//Retrive data from  Sp using oracle query.
     public static  List<DSAPageObject> retriveQuoteList = new List<DSAPageObject>();//retrive from pnrapilog and store it in list

        // Store Quot Number
       // List<RequestParams> quoteNumber = new List<RequestParams>();
      public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
       // private object retriveSmartPriceData;

        static OracleDBConnection()
        {
            ProdOradb = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST =smprpr2dbscn.us.dell.com)(PORT = 1521))" +
                 "(LOAD_BALANCE = yes)" +
                "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME =smprp_rw_oud.prd.amer.dell.com)));" +
                "User Id=smp;Password=smp_us3rprd;";

            ReportProdOradb=
                 "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST =SMPRPR1DBSCN.US.DELL.COM)(PORT = 1521))" +
                 "(LOAD_BALANCE = yes)" +
                "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME =SMPRP_RW_OUD.PRD.AMER.DELL.COM)));" +
                "User Id=smp;Password=smp_us3rprd;";

            g4Oradb = "Data Source=(DESCRIPTION ="+
                "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.161.9.81)(PORT = 1521))" +
                "(CONNECT_DATA ="+"(SERVER = DEDICATED)"+
                "(SERVICE_NAME = smp2s.sit.amer.dell.com)));" +
                 "User Id=smp;Password=smp_us3r;";



            
            g2Oradb = "Data Source=(DESCRIPTION ="+
                    "(ADDRESS = (PROTOCOL = TCP)(HOST = ausulsmpdb02.us.dell.com)(PORT = 1521))" +
                    "(CONNECT_DATA ="+
                    "(SERVER = DEDICATED)"+
                    "(SERVICE_NAME = sp211s.sit.amer.dell.com)));" +
                    "User Id=smp;Password=smp_us3r;";


        }


        public DSAPageObject RetriveQuote(DSAPageObject dsaObject,string condition)
        {
           
            OracleCommand command = new OracleCommand();
            command.CommandText = "select  NVL(QuoteNumber,''),NVL(QuoteVersionNumber,''),NVL(LOCALCHANNELID,''),NVL(ORDERCODE,'') from PNRAPILOG pnr "+condition+ " and rownum <= 1";
            //  command.CommandText = "select  NVL(QuoteNumber,''),NVL(QuoteVersionNumber,''),NVL(LOCALCHANNELID,''),NVL(ORDERCODE,'') from PNRAPILOG where CREATED > TO_DATE('06/14/2021', 'MM/DD/YYYY') and georegion = 'Americas' and geocountry = 'United States'and QuoteNumber Is not null  and OrderCode Is not Null and QUOTINGSYSTEM ='DSP' and rownum <= 10 ";
            // command.CommandText = "select  NVL(QuoteNumber,''),NVL(QuoteVersionNumber,''),NVL(LOCALCHANNELID,''),NVL(ORDERCODE,'') from PNRAPILOG "++;

            command.Connection = con;
           
            try
            {
                con.Open();
                OracleDataReader dr = command.ExecuteReader();
                //List<DSAPageObject> li = new List<DSAPageObject>();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dsaObject.ActualQuote = GetValueIgnoringNull(dr.GetString(0));//retrive the quote number 
                        dsaObject.QuoteVersionNumber = GetValueIgnoringNull(dr.GetString(1));
                        dsaObject.LocalChannelId = GetValueIgnoringNull(dr.GetString(2));
                        dsaObject.OrderCode = GetValueIgnoringNull(dr.GetString(3));
                        dsaObject.ActualQuote = dsaObject.ActualQuote +"."+dsaObject.QuoteVersionNumber;
                        retriveQuoteList.Add(dsaObject);
                        
                       
                    }
                    
                    
                }
        // var quote= JsonSerializer.Serialize(dsaObject.QuoteNumber);


                con.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
          //  WriteDataInExcel();


            return dsaObject;

        }
        public string RetriveOrderCode(string productCondition,string localChannelId)
        {
            string orderCode = "";
            OracleCommand command = new OracleCommand();
            
            command.CommandText = "select ordercode from smp.pnrapilog where localchannelid='" + localChannelId+ "' and " + productCondition;
            

            command.Connection = con;

            try
            {
                con.Open();
                OracleDataReader dr = command.ExecuteReader();
                try
                {
                   
                    if (dr.Read())
                    {
                       
                        orderCode = GetValueIgnoringNull(dr.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("OrderCode is not found" + ex.Message);
                }

                con.Close();
            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return orderCode;
        }
        public string RetriveOrderCode(string productCondition)
        {
            string orderCode = "";
            OracleCommand command = new OracleCommand();

            command.CommandText = "select ordercode from smp.pnrapilog"  + productCondition;
            command.Connection = con;

            try
            {
                con.Open();
                OracleDataReader dr = command.ExecuteReader();
                try
                {

                    if (dr.Read())
                    {

                        orderCode = GetValueIgnoringNull(dr.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("OrderCode is not found" + ex.Message);
                }

                con.Close();
            }

            catch (OracleException e)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage($"OracleException is showing { e}");
            }
            catch (Exception e)
            {

                throw new ShowStopperException("Unable to RetriveOrderCode()", e);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return orderCode;
        }

        //d02
        public  void RetriveBaseProductQuote(DSAPageObject dsaObject,string productCondition,string quoteNumber)
        {
            
            OracleCommand command = new OracleCommand();
            //foreach (var list in retriveQuoteList)
            //{




            // command.CommandText = "select  NVL(QuoteNumber,''), NVL(SKUNUM,''),NVL(LOCALCHANNELID,''),NVL(ORDERCODE,''),NVL(PRODUCTLOB,'') from PNRAPILOG" +condition ;
            //command.CommandText = "select  NVL(QuoteNumber,''), NVL(SKUNUM,''),NVL(LOCALCHANNELID,''),NVL(ORDERCODE,''),NVL(PRODUCTLOB,'') from PNRAPILOG" + productCondition + " and ORDERCODE<>'" + ordercode + "'and LOCALCHANNELID='" + list.LocalChannelId + "'and rownum <= 1 and ";
            command.CommandText = "select ordercode from smp.pnrapilog where ordercode not in(select ordercode from smp.pnrapilog where quotenumber='" + quoteNumber + "' and created>sysdate-1)and rownum<2 and localchannelid='"+dsaObject.LocalChannelId+"' and " + productCondition;
            //  cmd.CommandText = "select NVL(BUID,' '), NVL(QUOTINGSYSTEM,' '), NVL(SSCCODE,' '), NVL(ACCOUNTID,' '), NVL(ENDUSERACCOUNTID,' '), NVL(DISTRIBUTOR,' '), NVL(LOCALCHANNELID,' '), NVL(PARTNERTYPE,' '), NVL(SFDCDEALID,' '), NVL(SFDCOpptyBookedDate,' '), NVL(SFDCDealTypeC,' '),NVL(SFDCOpptyStage,' '), NVL(FullCatalog,' '), NVL(SegmentInfo,' '), NVL(QuoteNumber,' '), NVL(QuoteVersionNumber,' '), NVL(Direct,' '), NVL(Quantity,' '), NVL(QuoteSize,' '), NVL(QuoteSizeLocalCurrency,' '), NVL(PartnerRelationshipLevel,' '), NVL(SalesRepID,' '), NVL(SalesRepBadgeNum,' '), NVL(SmartSelect,' '), NVL(QuoteCountry,' '), NVL(ContractCode,' '), NVL(ProductCertificationLevel,' '), NVL(QuoteCurrency,' '), NVL(Registered,' '), NVL(DiscountedPricePerUnit,' '), NVL(ListMarginPCT,' '),NVL(costPerUnit,' '), NVL(ItemClassCode,' '), NVL(SKUNum,' '), NVL(OpportunitySize,' '), NVL(ProductFamily,' '), NVL(OrderCode,' '), NVL(ListPricePerUnit,' '), NVL(Lease,' '), NVL(AccountCustomerID,' '), NVL(EndUserAccountCustomerID,' '), NVL(CSPAccountCustomerID,' '), NVL(CSPAccountID,' '), NVL(OrderID,' '), NVL(DPID,' '), NVL(OrderFormID,' '), NVL(OrderGroupID,' '), NVL(SFDCFulfilmentPath,' '), NVL(SFDCDISTIACCOUNTID,' '), NVL(ServiceType,' '),NVL(SMARTPRICEID,' '),NVL(ITEMCLASSCODEDESC,' '),NVL(SKUNUMDESC,' '),NVL(DELLCLASSCODE,' '),NVL(SUBCLASSCODE,' '),NVL(MANUFACTURECODE,' '),NVL(BULOCALCHANNEL,' '),NVL(SLSBULVL4,' '),NVL(GEOBU,' '),NVL(GEOCOUNTRY,' '),NVL(ISREGISTERED,' '),NVL(ISACCOUNT,' '),NVL(ISDIRECT,' '),NVL(ISPARTNER,' '),NVL(IsDistributor,' '),NVL(DealSize,' '),NVL(ENDUSERACCOUNTEXISTS,' '),NVL(ACCOUNTGROUP,' '),NVL(AccountGroupHierarchy,' '),NVL(PartnerRelationshipLevel,' '),NVL(IsSmallDealPath,' '),NVL(isGreenfieldOppty,' '),NVL(BuyingPowerCategory,' '),NVL(SalesCompensationRole,' '),NVL(KICKERTYPES,' '),NVL(ENDUSERACCOUNTRATING,' '),NVL(SegmentLocalChannelID,' '),NVL(SLSBusinessUnit,' '),NVL(SLSBULVL1,' '),NVL(SLSBULVL2,' '),NVL(SLSBULVL3,' '),NVL(ProductType,' '),NVL(PRODUCTGROUP,' '),NVL(ProductLOB,' '),NVL(ProductBrandGroup,' '),NVL(ProductBrandCategory,' '),NVL(ProductFamilyParent,' '),NVL(PRODUCTFAMILYDESC,' '),NVL(ProductOffering,' '),NVL(GeoRegion,' '),NVL(GeoSubRegion,' '),NVL(GeoArea,' '),NVL(SegmentNo,' '),NVL(ExchangeRate,' '),NVL(TargetAmount,' '),NVL(CONV_OPPORTUNITYSIZE,' '),NVL(CONV_QUOTESIZE,' '),NVL(CONV_LISTPRICEPERUNIT,' '),NVL(CONV_DISCOUNTEDPRICEPERUNIT,' '),NVL(SIC1Code,' '),NVL(SIC2Code,' '),NVL(SIC3Code,' '),NVL(LISTMARGINPCT,' '),NVL(CONV_COSTPERUNIT,' '),NVL(ADDITIONALINFO,' '),NVL(ERRORS,' '),NVL(RECOMMENDEDDOLPCT,' '),NVL(COMPANCHORDOLPCT,' '),NVL(FLOORDOLPCT,' '),NVL(StandardPartnerDiscount,' '),NVL(StandardPartnerMargin,' '),NVL(BaseMultiplier,' '),NVL(CompAccelerator,' '),NVL(CompDecelerator,' '),NVL(MarginRecommended,' '),NVL(MarginCompAnchor,' '),NVL(MarginFloor,' '),NVL(LeaseModifier,' '),NVL(CompModMin,' '),NVL(CompModMax,' '),NVL(COMPMAXACCEL,' '),NVL(COMPMINDECEL,' '),NVL(REBATESTHRESHOLD,' '),NVL(RebateType,' '),NVL(IsRebateAvailable,' '),NVL(COMMMOD1,' '),NVL(COMMMOD2,' '),NVL(COMMMOD3,' '),NVL(COMMMOD4,' ') ,NVL(ENDUSERLOCALCHANNELID,' '),NVL(PRODUCTFAMILY,' '),NVL(SFDCUNASSIGNEDENDUSERC,' '),NVL(SFDCOPPTYPROBABILITY,' '),NVL(SFDCTYPE,' '),NVL(SFDCDEALREGOPPTY,' '),NVL(SFDCREGISTRATIONSTATUS,' '),NVL(SFDCOPPTYRECORDTYPE,' '),NVL(QUOTEACCOUNTID,' '),NVL(QUOTEENDUSERACCOUNTID,' '),NVL(PAYMENTMETHOD,' '),NVL(PAYMENTTERMS,' ') from PNRAPILOG where QuoteNumber=";

            command.Connection = con;

                try
                {
                    con.Open();
                    OracleDataReader dr = command.ExecuteReader();
                   try
                    {
                    
                        
                            if (dr.Read())
                            {
                                //dsaObject.QuoteNumber = GetValueIgnoringNull(dr.GetString(0));//retrive the quote number 
                                //dsaObject.SKUNumber = GetValueIgnoringNull(dr.GetString(1));
                                //dsaObject.LocalChannelId = GetValueIgnoringNull(dr.GetString(2));
                                dsaObject.OrderCode = GetValueIgnoringNull(dr.GetString(0));
                                //dsaObject.ProductLob = GetValueIgnoringNull(dr.GetString(3));
                                //Constant.StoreOrderDetails.Add(dsaObject);
                                //baseProductQuotes.Add(dsaObject);
                            }




                        
                    }
                     catch(Exception ex)
                {
                    Console.WriteLine("OrderCode is not found" +ex.Message);
                }
                   
                    con.Close();
                }

                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            
        }
        /// <summary>
        /// Retrive SFDC deal Id
        /// </summary>
        /// <param name="dsaObject"></param>
        /// <param name="getServiceTypeQuote"></param>
        public void RetriveSFDCId(DSAPageObject dsaObject, string getServiceTypeQuote)
        {
            try
            {
                cmd.CommandText = "select To_char(created,'mm/dd/yyyy HH:MI:SS PM')created,SFDCDEALID from smp.pnrapilog where quotenumber='3000094279685' and created> sysdate-1";
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        
                        dsaObject.QuoteNumber = GetValueIgnoringNull(dr.GetString(0));
                        dsaObject.PnrLog.SFDCDEALID = GetValueIgnoringNull(dr.GetString(1));
                      
                    }

                }

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }


        }
        //Retrive quote details from Smartprice using SKU and draft quote number.
        private string GetValueIgnoringNull(String value)
        {
            if (string.IsNullOrEmpty(value) || value=="Empty"||value=="null")
                return "";
            else
                return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsaObject"></param>
        /// <param name="getService"></param>
        public void RetriveServiceTypeOneQuote(DSAPageObject dsaObject,string getServiceTypeQuote)
        {
            try
            {
                cmd.CommandText = "select servicetype,quotenumber, pnr.quoteversionnumber,skunum, ordercode, pnr.* from smp.pnrapilog pnr" + getServiceTypeQuote;
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    if(dr.Read())
                    {
                        dsaObject.PnrLog.ServiceType = GetValueIgnoringNull(dr.GetString(0));
                        dsaObject.ActualQuote = GetValueIgnoringNull(dr.GetString(1));
                      // dsaObject.QuoteVersionNumber = GetValueIgnoringNull(dr.GetString(2));
                        dsaObject.ActualQuote = dsaObject.ActualQuote + ".1";
                        dsaObject.SKUNumber= GetValueIgnoringNull(dr.GetString(3));
                        dsaObject.OrderCode = GetValueIgnoringNull(dr.GetString(4));

                    }

                }

                con.Close();
            }
            
            
            catch (OracleException ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage(ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage(ex.StackTrace);
            }
            catch (Exception ex)
            {
                new ShowStopperException("Unable to find read data from Pnrapilog , please verify RetriveServiceTypeOneQuote() in OracleDBConnection",ex);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }


        }
        public CustomerClass RetriveEndCustomerDetailsFromCurrentDraftQuote(string quotenum,CustomerClass customerObj)
        {
            try
            {
                cmd.CommandText = "select NVL(accountcustomerid,'Empty'),NVL(accountid,'Empty'),NVL(enduseraccountcustomerid,'Empty'),NVL(enduseraccountid,'Empty') ,NVL(LocalChannelId,'EMPTY'),NVL(QuoteAccountId,'EMPTY')from smp.pnrapilog where quotingsystem='DSP'and quotenumber='" + quotenum + "'";
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                
                    if (dr.Read())
                    {
                        customerObj.OldAccountCustomerId = GetValueIgnoringNull(dr.GetString(0));
                        customerObj.OldAccountId = GetValueIgnoringNull(dr.GetString(1));
                        customerObj.OldEndUserAccountCustomerId = GetValueIgnoringNull(dr.GetString(2));
                        customerObj.OldEndUserAccountId = GetValueIgnoringNull(dr.GetString(3));
                        customerObj.CrossSegmenId = GetValueIgnoringNull(dr.GetString(4));
                        customerObj.OldQuoteAccountId = GetValueIgnoringNull(dr.GetString(5));
                }
                
               
            }
            catch (OracleException ex)
            {
               Bedrock.Utilities.SimpleLogger.LogMessage(ex.Message);
            }
            
            catch (Exception ex)
            {
                throw new ShowStopperException("Unable to retrive data from  RetriveEndCustomerDetailsFromCurrentDraftQuote() in OracleDBConnectionClass", ex);
                
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return customerObj;
        }
        public DSAPageObject RetriveSFDCDealId(string quotenum, DSAPageObject sfdcObj,CustomerClass customerObj,string getsfdcDealIdCondition)
        {
            try
            {
                if (string.IsNullOrEmpty(customerObj.OldQuoteAccountId) || customerObj.OldQuoteAccountId == "0")
                {
                    cmd.CommandText = "select NVL(QuoteAccountId,'Empty'),NVL(SFDCDEALID,'Empty'),NVL(SFDCOPPTYBOOKEDDATE,''),NVL(CREATED,''),NVL(DEALSIZE,'Empty'),NVL(OPPORTUNITYSIZE,'Empty'),NVL(SFDCOPPTYSTAGE,'Empty'),NVL(ADDITIONALINFO,'Empty'),NVL(QUOTESIZE,'Empty')from smp.pnrapilog " + getsfdcDealIdCondition + " and quotenumber<>'" + quotenum + "' and sfdcdealid is not null";
                }
                else
                {
                    cmd.CommandText = "select NVL(QuoteAccountId,'Empty'),NVL(SFDCDEALID,'Empty'),NVL(SFDCOPPTYBOOKEDDATE,''),NVL(CREATED,''),NVL(DEALSIZE,'Empty'),NVL(OPPORTUNITYSIZE,'Empty'),NVL(SFDCOPPTYSTAGE,'Empty'),NVL(ADDITIONALINFO,'Empty'),NVL(QUOTESIZE,'Empty')from smp.pnrapilog " + getsfdcDealIdCondition + " and quotenumber<>'" + quotenum + "' and QuoteAccountId='" + customerObj.OldQuoteAccountId + "' sfdcdealid is not null";
                }
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    customerObj.OldQuoteAccountId = GetValueIgnoringNull(dr.GetString(0));
                    sfdcObj.PnrLog.SFDCDEALID = GetValueIgnoringNull(dr.GetString(1));
                    sfdcObj.PnrLog.SFDCOPPTYBOOKEDDATE = GetValueIgnoringNull(dr.GetString(2));
                    sfdcObj.CreatedDate = GetValueIgnoringNull(dr.GetString(3));
                    sfdcObj.DealSize = GetValueIgnoringNull(dr.GetString(4));
                    sfdcObj.PnrLog.OPPORTUNITYSIZE = GetValueIgnoringNull(dr.GetString(5));
                    sfdcObj.PnrLog.SFDCOPPTYSTAGE= GetValueIgnoringNull(dr.GetString(6));
                    sfdcObj.PnrLog.AdditionalInfo = GetValueIgnoringNull(dr.GetString(7));
                    sfdcObj.QuoteSize = GetValueIgnoringNull(dr.GetString(8));

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return sfdcObj;
        }
        public DSAPageObject RetriveSFDCDealDetails(string quotenum, DSAPageObject sfdcObj, CustomerClass customerObj)
        {
            try
            {
                cmd.CommandText = "select NVL(QuoteAccountId,'Empty'),NVL(SFDCDEALID,'Empty'),NVL(SFDCOPPTYBOOKEDDATE,''),NVL(CREATED,''),NVL(DEALSIZE,'Empty'),NVL(OPPORTUNITYSIZE,'Empty'),NVL(SFDCOPPTYSTAGE,'Empty'),NVL(ADDITIONALINFO,'Empty'),NVL(QUOTESIZE,'Empty')from smp.pnrapilog where quotingsystem='DSP' and SFDCDEALID='"+sfdcObj.PnrLog.SFDCDEALID+"'and rownum=1";
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    customerObj.OldQuoteAccountId = GetValueIgnoringNull(dr.GetString(0));
                    sfdcObj.PnrLog.SFDCDEALID = GetValueIgnoringNull(dr.GetString(1));
                    sfdcObj.PnrLog.SFDCOPPTYBOOKEDDATE = GetValueIgnoringNull(dr.GetString(2));
                    sfdcObj.CreatedDate = GetValueIgnoringNull(dr.GetString(3));
                    sfdcObj.DealSize = GetValueIgnoringNull(dr.GetString(4));
                    sfdcObj.PnrLog.OPPORTUNITYSIZE = GetValueIgnoringNull(dr.GetString(5));
                    sfdcObj.PnrLog.SFDCOPPTYSTAGE = GetValueIgnoringNull(dr.GetString(6));
                    sfdcObj.PnrLog.AdditionalInfo = GetValueIgnoringNull(dr.GetString(7));
                    sfdcObj.QuoteSize = GetValueIgnoringNull(dr.GetString(8));

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return sfdcObj;
        }
        public DSAPageObject RetriveSFDCGuidanceMessage(string quotenum, DSAPageObject sfdcObj)
        {
            try
            {
                cmd.CommandText = "select NVL(GUIDANCEMESSAGESJSON,'Empty')from smp.pnrapilog where quotingsystem='DSP' and SFDCDEALID='" + sfdcObj.PnrLog.SFDCDEALID + "' and rownum=1";
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    sfdcObj.PnrLog.GUIDANCEMESSAGESJSON= GetValueIgnoringNull(dr.GetString(0));
                    // sfdcObj.PnrLog.GUIDANCEMESSAGESJSON = GetValueIgnoringNull(dr.GetString(0));
                    //List<GuidanceMessage> guidanceMessages = JSONDeserializer.GetObject(dr.GetString(0));
                    //foreach(GuidanceMessage message in guidanceMessages)
                    //{
                    //    sfdcObj.PnrLog.GUIDANCEMESSAGESJSON.Add(message);
                    //}
                    

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return sfdcObj;
        }
        public string RetriveEnduserAccountCustomerIdWithoutQuoteNumber(string condition)
        {
            string endUserAccountCustomerId = "";
            try
            {
                
                cmd.CommandText = "select NVL(enduseraccountcustomerid,'EMPTY') from smp.pnrapilog " + condition;
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                
                    if (dr.Read())
                    {
                        endUserAccountCustomerId = GetValueIgnoringNull(dr.GetString(0));
                        
                    }
                

            }
            catch (OracleException ex)
            {
                new ShowStopperException("Oracle error comes up ,please refer exception Message ", ex);
            }
            catch (Exception ex)
            {
                new ShowStopperException("Unable to get 'enduseraccountcustomerid'.Please check StackTrace ", ex);
            }
            
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return endUserAccountCustomerId;
        }
        public CustomerClass RetriveIndirectCustomerDetails(string condition,CustomerClass customerObj,PNRAPILog pnrObject)
        {
           
            try
            {
                cmd.CommandText = "select NVL(accountcustomerid,'EMPTY'),NVL(accountid,'EMPTY'),NVL(enduseraccountcustomerid,'EMPTY'),NVL(enduseraccountid,'EMPTY'),NVL(SFDCDEALID,'Empty'),NVL(SFDCUNASSIGNEDENDUSERC,'Empty'),NVL(SFDCOPPTYPROBABILITY,'Empty'),NVL(SFDCREGISTRATIONSTATUS,'Empty'),NVL(SFDCFULFILMENTPATH,'Empty'),NVL(SFDCDEALTYPEC,'Empty'),NVL(SFDCOPPTYSTAGE,'Empty'),NVL(SFDCOPPTYBOOKEDDATE,'Empty'),NVL(SFDCDEALREGOPPTY,'Empty'),NVL(SFDCOPPTYRECORDTYPE,'Empty'),NVL(SFDCDISTIACCOUNTID,'Empty'),NVL(SFDCDISTIACCOUNTID,'Empty') from smp.pnrapilog " + condition;
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                
                    if (dr.Read())
                    {
                        customerObj.OldAccountCustomerId = GetValueIgnoringNull(dr.GetString(0));
                        customerObj.OldAccountId = GetValueIgnoringNull(dr.GetString(1));
                        customerObj.OldEndUserAccountCustomerId = GetValueIgnoringNull(dr.GetString(2));
                        customerObj.OldEndUserAccountId = GetValueIgnoringNull(dr.GetString(3));
                        pnrObject.SFDCDEALID = GetValueIgnoringNull(dr.GetString(4));
                        pnrObject.SFDCUNASSIGNEDENDUSERC = GetValueIgnoringNull(dr.GetString(5));
                        pnrObject.SFDCOPPTYPROBABILITY = GetValueIgnoringNull(dr.GetString(6));
                        pnrObject.SFDCREGISTRATIONSTATUS = GetValueIgnoringNull(dr.GetString(7));
                        pnrObject.SFDCFULFILMENTPATH = GetValueIgnoringNull(dr.GetString(8));
                        pnrObject.SFDCDEALTYPEC = GetValueIgnoringNull(dr.GetString(9));
                        pnrObject.SFDCOPPTYSTAGE = GetValueIgnoringNull(dr.GetString(10));
                        pnrObject.SFDCOPPTYBOOKEDDATE = GetValueIgnoringNull(dr.GetString(11));
                        pnrObject.SFDCDEALREGOPPTY = GetValueIgnoringNull(dr.GetString(12));
                        pnrObject.SFDCOPPTYRECORDTYPE = GetValueIgnoringNull(dr.GetString(13));
                        pnrObject.SFDCDISTIACCOUNTID = GetValueIgnoringNull(dr.GetString(14));
                        
                    }
                

            }
            catch (OracleException ex)
            {
                throw new ShowStopperException("Oracle Exception is occured",ex);
            }
            catch (Exception ex)
            {
                throw new ShowStopperException("Unable to read results from table , Refer RetriveIndirectCustomerDetails() in OracleDBConnection class ", ex);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return customerObj;
        }
        public PNRAPILog RetriveSFDCDetails(string quotenum,PNRAPILog pnrObject)
        {
            string[] quote = quotenum.Split('.');
            string quoteNo = quote[0];
            try
            {
                cmd.CommandText = "select NVL(SFDCDEALID,'Empty'),NVL(SFDCUNASSIGNEDENDUSERC,'Empty'),NVL(SFDCOPPTYPROBABILITY,'Empty'),NVL(SFDCREGISTRATIONSTATUS,'Empty'),NVL(SFDCFULFILMENTPATH,'Empty'),NVL(SFDCDEALTYPEC,'Empty'),NVL(SFDCOPPTYSTAGE,'Empty'),NVL(SFDCOPPTYBOOKEDDATE,'Empty'),NVL(SFDCDEALREGOPPTY,'Empty'),NVL(SFDCOPPTYRECORDTYPE,'Empty'),NVL(SFDCDISTIACCOUNTID,'Empty') from smp.pnrapilog where quotenumber='"+quoteNo+ "' and rownum=1 order by created desc ";
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    
                    pnrObject.SFDCDEALID = GetValueIgnoringNull(dr.GetString(0)).ToLower();
                    pnrObject.SFDCUNASSIGNEDENDUSERC = GetValueIgnoringNull(dr.GetString(1)).ToLower();
                    pnrObject.SFDCOPPTYPROBABILITY = GetValueIgnoringNull(dr.GetString(2)).ToLower();
                    pnrObject.SFDCREGISTRATIONSTATUS = GetValueIgnoringNull(dr.GetString(3)).ToLower();
                    pnrObject.SFDCFULFILMENTPATH = GetValueIgnoringNull(dr.GetString(4)).ToLower();
                    pnrObject.SFDCDEALTYPEC = GetValueIgnoringNull(dr.GetString(5)).ToLower();
                    pnrObject.SFDCOPPTYSTAGE = GetValueIgnoringNull(dr.GetString(6)).ToLower();
                    pnrObject.SFDCOPPTYBOOKEDDATE = GetValueIgnoringNull(dr.GetString(7)).ToLower();
                    pnrObject.SFDCDEALREGOPPTY = GetValueIgnoringNull(dr.GetString(8)).ToLower();
                    pnrObject.SFDCOPPTYRECORDTYPE = GetValueIgnoringNull(dr.GetString(9)).ToLower();
                    pnrObject.SFDCDISTIACCOUNTID = GetValueIgnoringNull(dr.GetString(10)).ToLower();
                }


            }
            catch (Exception ex)
            {
                throw new ShowStopperException(string.Format("Unable to Retrive SFDCDetails", ex));
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pnrObject;
        }
        public CustomerClass RetriveEndUserInformation(string quotenum, CustomerClass customerObj)
        {
            try
            {
                string[] quotenumber = quotenum.Split('.');
                string withoutversionquote = quotenumber[0];
                cmd.CommandText = "select NVL(accountcustomerid,'EMPTY'),NVL(accountid,'EMPTY'),NVL(enduseraccountcustomerid,'EMPTY'),NVL(enduseraccountid,'EMPTY') from smp.pnrapilog where quotenumber='" + withoutversionquote + "' and quotingsystem='DSP'";
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        customerObj.OldAccountCustomerId = GetValueIgnoringNull(dr.GetString(0));
                        customerObj.OldAccountId = GetValueIgnoringNull(dr.GetString(1));
                        customerObj.OldEndUserAccountCustomerId = GetValueIgnoringNull(dr.GetString(2));
                        customerObj.OldEndUserAccountId = GetValueIgnoringNull(dr.GetString(3));
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new ShowStopperException(string.Format("Unable to RetriveEndUserInformation {0} ",ex));
            }
            
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return customerObj;
        }
        public CustomerClass RetriveEndCustomerDetailsNotInCurrentQuote(string quotenum, CustomerClass customerObj, string condition)
        {
            try
            {
                if(string.IsNullOrEmpty(customerObj.OldQuoteAccountId)||customerObj.OldQuoteAccountId=="0")
                {
                    cmd.CommandText = "select NVL(accountcustomerid,'EMPTY'),NVL(accountid,'EMPTY'),NVL(enduseraccountcustomerid,'EMPTY'),NVL(enduseraccountid,'EMPTY') from smp.pnrapilog where quotenumber<>'" + quotenum + "' and LOCALCHANNELID='" + customerObj.CrossSegmenId + "'and quotingsystem='DSP'";
                }
                
                else
                {
                    cmd.CommandText = "select NVL(accountcustomerid,'EMPTY'),NVL(accountid,'EMPTY'),NVL(enduseraccountcustomerid,'EMPTY'),NVL(enduseraccountid,'EMPTY') from smp.pnrapilog where quotenumber<>'" + quotenum + "' and LOCALCHANNELID='" + customerObj.CrossSegmenId + "'and quotingsystem='DSP' and QUOTEACCOUNTID<>'" + customerObj.OldQuoteAccountId + "'";
                }
                cmd.Connection = con;
                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        customerObj.NewAccountCustomerId = GetValueIgnoringNull(dr.GetString(0));
                        customerObj.NewAccountId = GetValueIgnoringNull(dr.GetString(1));
                        customerObj.NewEndUserAccountCustomerId = GetValueIgnoringNull(dr.GetString(2));
                        customerObj.NewEndUserAccountId = GetValueIgnoringNull(dr.GetString(3));
                    }
                }

            }
            catch (OracleException ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Oracle exception occured in  RetriveEndCustomerDetailsNotInCurrentQuote()");
            }
            catch (Exception ex)
            {
             throw  new ShowStopperException("Unable to retrive RetriveEndCustomerDetailsNotInCurrentQuote()", ex);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return customerObj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <param name="guidance"></param>
        /// <param name="dr"></param>
        /// <param name="compareobject"></param>
        /// <param name="isNewRequest"></param>
        /// <param name="overrideQuotingSystem"></param>
        /// <param name="quotingsystemName"></param>
  
        public PNRAPILog RetriveSmartPriceDetails(string quoteNum, string skuNum, DSAPageObject dsa)
        {
             
     //       SmartPrice_E2E_WebAutomation.Quote.D13.ScenarioMain D13Object = new Quote.D13.ScenarioMain(null);
            //cmd.CommandText = "select NVL(BUID, ' '), NVL(QUOTINGSYSTEM, ' '), NVL(SSCCODE, ' '), NVL(ACCOUNTID, ' '), NVL(ENDUSERACCOUNTID, ' '), NVL(DISTRIBUTOR, ' '), NVL(LOCALCHANNELID, ' '), NVL(PARTNERTYPE, ' '), NVL(SFDCDEALID, ' '), NVL(SFDCOpptyBookedDate, ' '), NVL(SFDCDealTypeC, ' '),NVL(SFDCOpptyStage, ' '), NVL(FullCatalog, ' '), NVL(SegmentInfo, ' '), NVL(QuoteNumber, ' '), NVL(QuoteVersionNumber, ' '), NVL(Direct, ' '), NVL(Quantity, ' '), NVL(QuoteSize, ' '), NVL(QuoteSizeLocalCurrency, ' '), NVL(PartnerRelationshipLevel, ' '), NVL(SalesRepID, ' '), NVL(SalesRepBadgeNum, ' '), NVL(SmartSelect, ' '), NVL(QuoteCountry, ' '), NVL(ContractCode, ' '), NVL(ProductCertificationLevel, ' '), NVL(QuoteCurrency, ' '), NVL(Registered, ' '), NVL(DiscountedPricePerUnit, ' '), NVL(ListMarginPCT, ' '),NVL(costPerUnit, ' '), NVL(ItemClassCode, ' '), NVL(SKUNum, ' '), NVL(OpportunitySize, ' '), NVL(ProductFamily, ' '), NVL(OrderCode, ' '), NVL(ListPricePerUnit, ' '), NVL(Lease, ' '), NVL(AccountCustomerID, ' '), NVL(EndUserAccountCustomerID, ' '), NVL(CSPAccountCustomerID, ' '), NVL(CSPAccountID, ' '), NVL(OrderID, ' '), NVL(DPID, ' '), NVL(OrderFormID, ' '), NVL(OrderGroupID, ' '), NVL(SFDCFulfilmentPath, ' '), NVL(SFDCDISTIACCOUNTID, ' '), NVL(ServiceType, ' '),NVL(SMARTPRICEID, ' '),NVL(ITEMCLASSCODEDESC, ' '),NVL(SKUNUMDESC, ' '),NVL(DELLCLASSCODE, ' '),NVL(SUBCLASSCODE, ' '),NVL(MANUFACTURECODE, ' '),NVL(BULOCALCHANNEL, ' '),NVL(SLSBULVL4, ' '),NVL(GEOBU, ' '),NVL(GEOCOUNTRY, ' '),NVL(ISREGISTERED, ' '),NVL(ISACCOUNT, ' '),NVL(ISDIRECT, ' '),NVL(ISPARTNER, ' '),NVL(IsDistributor, ' '),NVL(DealSize, ' '),NVL(ENDUSERACCOUNTEXISTS, ' '),NVL(ACCOUNTGROUP, ' '),NVL(AccountGroupHierarchy, ' '),NVL(PartnerRelationshipLevel, ' '),NVL(IsSmallDealPath, ' '),NVL(isGreenfieldOppty, ' '),NVL(BuyingPowerCategory, ' '),NVL(SalesCompensationRole, ' '),NVL(KICKERTYPES, ' '),NVL(ENDUSERACCOUNTRATING, ' '),NVL(SegmentLocalChannelID, ' '),NVL(SLSBusinessUnit, ' '),NVL(SLSBULVL1, ' '),NVL(SLSBULVL2, ' '),NVL(SLSBULVL3, ' '),NVL(ProductType, ' '),NVL(PRODUCTGROUP, ' '),NVL(ProductLOB, ' '),NVL(ProductBrandGroup, ' '),NVL(ProductBrandCategory, ' '),NVL(ProductFamilyParent, ' '),NVL(PRODUCTFAMILYDESC, ' '),NVL(ProductOffering, ' '),NVL(GeoRegion, ' '),NVL(GeoSubRegion, ' '),NVL(GeoArea, ' '),NVL(SegmentNo, ' '),NVL(ExchangeRate, ' '),NVL(TargetAmount, ' '),NVL(CONV_OPPORTUNITYSIZE, ' '),NVL(CONV_QUOTESIZE, ' '),NVL(CONV_LISTPRICEPERUNIT, ' '),NVL(CONV_DISCOUNTEDPRICEPERUNIT, ' '),NVL(SIC1Code, ' '),NVL(SIC2Code, ' '),NVL(SIC3Code, ' '),NVL(LISTMARGINPCT, ' '),NVL(CONV_COSTPERUNIT, ' '),NVL(ADDITIONALINFO, ' '),NVL(ERRORS, ' '),NVL(RECOMMENDEDDOLPCT, ' '),NVL(COMPANCHORDOLPCT, ' '),NVL(FLOORDOLPCT, ' '),NVL(StandardPartnerDiscount, ' '),NVL(StandardPartnerMargin, ' '),NVL(BaseMultiplier, ' '),NVL(CompAccelerator, ' '),NVL(CompDecelerator, ' '),NVL(MarginRecommended, ' '),NVL(MarginCompAnchor, ' '),NVL(MarginFloor, ' '),NVL(LeaseModifier, ' '),NVL(CompModMin, ' '),NVL(CompModMax, ' '),NVL(COMPMAXACCEL, ' '),NVL(COMPMINDECEL, ' '),NVL(REBATESTHRESHOLD, ' '),NVL(RebateType, ' '),NVL(IsRebateAvailable, ' '),NVL(COMMMOD1, ' '),NVL(COMMMOD2, ' '),NVL(COMMMOD3, ' '),NVL(COMMMOD4, ' ') ,NVL(ENDUSERLOCALCHANNELID, ' '),NVL(PRODUCTFAMILY, ' '),NVL(SFDCUNASSIGNEDENDUSERC, ' '),NVL(SFDCOPPTYPROBABILITY, ' '),NVL(SFDCTYPE, ' '),NVL(SFDCDEALREGOPPTY, ' '),NVL(SFDCREGISTRATIONSTATUS, ' '),NVL(SFDCOPPTYRECORDTYPE, ' '),NVL(QUOTEACCOUNTID, ' '),NVL(QUOTEENDUSERACCOUNTID, ' '),NVL(PAYMENTMETHOD, ' '),NVL(PAYMENTTERMS, ' ') from PNRAPILOG where quotenumber=" + quoteNum + "and skunum=" + skuNum + "";
            // cmd.CommandText = "select NVL(LISTPRICEPERUNIT, 'Empty'),NVL(DISCOUNTEDPRICEPERUNIT, ' '),NVL(COSTPERUNIT,'Empty'),NVL(RECOMMENDEDDOLPCT,''),NVL(COMPANCHORDOLPCT,''),NVL(FLOORDOLPCT,''),NVL(COMMMOD1,''),NVL(COMMMOD2,''),NVL(COMMMOD3,''),NVL(COMMMOD4,''),NVL(COMPDECELERATOR,''),NVL(COMPACCELERATOR,''),NVL(COMPMODMIN,''),NVL(COMPMODMAX,''),NVL(STANDARDPARTNERDISCOUNT,'Empty'),NVL(STANDARDPARTNERMARGIN,'Empty'),NVL(BASEMULTIPLIER,''),NVL(LEASEMODIFIER,''),NVL(MARGINRECOMMENDED,''),NVL(MARGINCOMPANCHOR,''),NVL(MARGINFLOOR,''),NVL(QUANTITY,''),NVL(QUOTENUMBER,''),NVL(SKUNUM,''),NVL(SMARTPRICEID,''),NVL(COMPMAXACCEL,''),NVL(COMPMINDECEL,''),NVL(REBATESTHRESHOLD,''),NVL(ISREBATEAVAILABLE,''),NVL(REBATETYPE,'Empty') from smp.PNRAPILOG where QUOTENUMBER = '" + quoteNum + "' and SKUNUM='"+skuNum+"' and CREATED > TO_DATE('05/26/2021', 'MM/DD/YYYY')";
            // cmd.CommandText= "Select NVL(LISTPRICEPERUNIT, 'Empty'),NVL(DISCOUNTEDPRICEPERUNIT, ' Empty'),NVL(COSTPERUNIT,'Empty'),NVL(RECOMMENDEDDOLPCT,'Empty'),NVL(COMPANCHORDOLPCT,'Empty'),NVL(FLOORDOLPCT,'Empty'),NVL(COMMMOD1,'Empty'),NVL(COMMMOD2,'Empty'),NVL(COMMMOD3,'Empty'),NVL(COMMMOD4,'Empty'),NVL(COMPDECELERATOR,'Empty'),NVL(COMPACCELERATOR,'Empty'),NVL(COMPMODMIN,'Empty'),NVL(COMPMODMAX,'Empty'),NVL(STANDARDPARTNERDISCOUNT,'Empty'),NVL(STANDARDPARTNERMARGIN,'Empty'),NVL(BASEMULTIPLIER,'Empty'),NVL(LEASEMODIFIER,'Empty'),NVL(MARGINRECOMMENDED,'Empty'),NVL(MARGINCOMPANCHOR,'Empty'),NVL(MARGINFLOOR,'Empty'),NVL(QUANTITY,'Empty'),NVL(QUOTENUMBER,'Empty'),NVL(SKUNUM,'Empty'),NVL(SMARTPRICEID,'Empty'),NVL(COMPMAXACCEL,'Empty'),NVL(COMPMINDECEL,'Empty'),NVL(REBATESTHRESHOLD,'Empty'),NVL(ISREBATEAVAILABLE,'Empty'),NVL(REBATETYPE,'Empty'),NVL(SFDCDEALID,'Empty'),NVL(SFDCUNASSIGNEDENDUSERC,'Empty'),NVL(SFDCOPPTYPROBABILITY,'Empty'),NVL(SFDCREGISTRATIONSTATUS,'Empty'),NVL(SFDCFULFILMENTPATH,'Empty'),NVL(SFDCDEALTYPEC,'Empty'),NVL(SFDCOPPTYSTAGE,'Empty'),NVL(SFDCOPPTYBOOKEDDATE,'Empty'),NVL(SFDCDEALREGOPPTY,'Empty'),NVL(SFDCOPPTYRECORDTYPE,'Empty'),NVL(SFDCDISTIACCOUNTID,'Empty'),NVL(APPLIEDPOLICIESJSON,''),NVL(SERVICETYPE,'') from smp.PNRAPILOG where QUOTENUMBER = '" + quoteNum + "' and SKUNUM='" + skuNum + "' and CREATED > SYSDATE-1  and rownum<2 order by Created desc";
         //   cmd.CommandText = "SELECT * FROM (SELECT NVL(LISTPRICEPERUNIT, 'Empty'),NVL(DISCOUNTEDPRICEPERUNIT, ' Empty'),NVL(COSTPERUNIT,'Empty'),NVL(RECOMMENDEDDOLPCT,'Empty'),NVL(COMPANCHORDOLPCT,'Empty'),NVL(FLOORDOLPCT,'Empty'),NVL(COMMMOD1,'Empty'),NVL(COMMMOD2,'Empty'),NVL(COMMMOD3,'Empty'),NVL(COMMMOD4,'Empty'),NVL(COMPDECELERATOR,'Empty'),NVL(COMPACCELERATOR,'Empty'),NVL(COMPMODMIN,'Empty'),NVL(COMPMODMAX,'Empty'),NVL(STANDARDPARTNERDISCOUNT,'Empty'),NVL(STANDARDPARTNERMARGIN,'Empty'),NVL(BASEMULTIPLIER,'Empty'),NVL(LEASEMODIFIER,'Empty'),NVL(MARGINRECOMMENDED,'Empty'),NVL(MARGINCOMPANCHOR,'Empty'),NVL(MARGINFLOOR,'Empty'),NVL(QUANTITY,'Empty'),NVL(QUOTENUMBER,'Empty'),NVL(SKUNUM,'Empty'),NVL(SMARTPRICEID,'Empty'),NVL(COMPMAXACCEL,'Empty'),NVL(COMPMINDECEL,'Empty'),NVL(REBATESTHRESHOLD,'Empty'),NVL(ISREBATEAVAILABLE,'Empty'),NVL(REBATETYPE,'Empty'),NVL(SFDCDEALID,'Empty'),NVL(SFDCUNASSIGNEDENDUSERC,'Empty'),NVL(SFDCOPPTYPROBABILITY,'Empty'),NVL(SFDCREGISTRATIONSTATUS,'Empty'),NVL(SFDCFULFILMENTPATH,'Empty'),NVL(SFDCDEALTYPEC,'Empty'),NVL(SFDCOPPTYSTAGE,'Empty'),NVL(SFDCOPPTYBOOKEDDATE,'Empty'),NVL(SFDCDEALREGOPPTY,'Empty'),NVL(SFDCOPPTYRECORDTYPE,'Empty'),NVL(SFDCDISTIACCOUNTID,'Empty'),NVL(APPLIEDPOLICIESJSON,'Empty'),NVL(SERVICETYPE,'Empty'),NVL(ISDIRECT,''),NVL(ISPARTNER,''),NVL(ISREGISTERED,''),NVL(STANDARDPARTNERDISCOUNT,'null'),TO_CHAR(created, 'MM/DD/YYYY HH:MI:SS PM')created from smp.PNRAPILOG where QUOTENUMBER = '" + quoteNum + "' and CREATED > SYSDATE - 1  order by Created desc) SUBQRY where rownum < 2 ";
            cmd.CommandText = "SELECT * FROM (SELECT NVL(LISTPRICEPERUNIT, 'Empty'),NVL(DISCOUNTEDPRICEPERUNIT, ' Empty'),NVL(COSTPERUNIT,'Empty'),NVL(RECOMMENDEDDOLPCT,'Empty'),NVL(COMPANCHORDOLPCT,'Empty'),NVL(FLOORDOLPCT,'Empty'),NVL(COMMMOD1,'Empty'),NVL(COMMMOD2,'Empty'),NVL(COMMMOD3,'Empty'),NVL(COMMMOD4,'Empty'),NVL(COMPDECELERATOR,'Empty'),NVL(COMPACCELERATOR,'Empty'),NVL(COMPMODMIN,'Empty'),NVL(COMPMODMAX,'Empty'),NVL(STANDARDPARTNERDISCOUNT,'Empty'),NVL(STANDARDPARTNERMARGIN,'Empty'),NVL(BASEMULTIPLIER,'Empty'),NVL(LEASEMODIFIER,'Empty'),NVL(MARGINRECOMMENDED,'Empty'),NVL(MARGINCOMPANCHOR,'Empty'),NVL(MARGINFLOOR,'Empty'),NVL(QUANTITY,'Empty'),NVL(QUOTENUMBER,'Empty'),NVL(SKUNUM,'Empty'),NVL(SMARTPRICEID,'Empty'),NVL(COMPMAXACCEL,'Empty'),NVL(COMPMINDECEL,'Empty'),NVL(REBATESTHRESHOLD,'Empty'),NVL(ISREBATEAVAILABLE,'Empty'),NVL(REBATETYPE,'Empty'),NVL(SFDCDEALID,'Empty'),NVL(SFDCUNASSIGNEDENDUSERC,'Empty'),NVL(SFDCOPPTYPROBABILITY,'Empty'),NVL(SFDCREGISTRATIONSTATUS,'Empty'),NVL(SFDCFULFILMENTPATH,'Empty'),NVL(SFDCDEALTYPEC,'Empty'),NVL(SFDCOPPTYSTAGE,'Empty'),NVL(SFDCOPPTYBOOKEDDATE,'Empty'),NVL(SFDCDEALREGOPPTY,'Empty'),NVL(SFDCOPPTYRECORDTYPE,'Empty'),NVL(SFDCDISTIACCOUNTID,'Empty'),NVL(APPLIEDPOLICIESJSON,'Empty'),NVL(SERVICETYPE,'Empty'),NVL(ISDIRECT,''),NVL(ISPARTNER,''),NVL(ISREGISTERED,''),NVL(STANDARDPARTNERDISCOUNT,'null'),TO_CHAR(created, 'MM/DD/YYYY HH:MI:SS PM')created from smp.PNRAPILOG where QUOTENUMBER = '" + quoteNum + "'and SKUNUM = '" + skuNum + "'  and CREATED > SYSDATE - 1  order by Created desc) SUBQRY where rownum < 2 ";

            PNRAPILog sp = new PNRAPILog();
            try
            { 
                cmd.Connection = con;
                con.Open();

                // sp.= dr.IsDBNull(1) ? 0 : dr.GetDecimal(1);
                OracleDataReader dr = cmd.ExecuteReader();
                //if (dr.HasRows)
                //{
                    if (dr.Read())
                    {

                        sp.ListPricePerUnit = Convert.ToDecimal(GetValueIgnoringNull(dr.GetString(0)));
                        sp.DiscountedPricePerUnit = Convert.ToDecimal(GetValueIgnoringNull(dr.GetString(1)));
                        sp.CostPerUnit = Convert.ToDecimal(GetValueIgnoringNull(dr.GetString(2)));
                        sp.SmartPriceGuidance.RecommendedDOLPercentage = GetValueIgnoringNull(dr.GetString(3)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.CompAnchorDOLPercentage = GetValueIgnoringNull(dr.GetString(4)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.FloorDOLPercentage = GetValueIgnoringNull(dr.GetString(5)).Replace(" ", "").Replace("%", "");

                        sp.SmartPriceGuidance.CommissionMod1 = GetValueIgnoringNull(dr.GetString(6)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.CommissionMod2 = GetValueIgnoringNull(dr.GetString(7)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.CommissionMod3 = GetValueIgnoringNull(dr.GetString(8)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.CommissionMod4 = GetValueIgnoringNull(dr.GetString(9)).Replace(" ", "").Replace("%", "");

                        sp.SmartPriceGuidance.CompDecelerator = GetValueIgnoringNull(dr.GetString(10)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.CompAccelerator = GetValueIgnoringNull(dr.GetString(11)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.CompModMin = GetValueIgnoringNull(dr.GetString(12)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.CompModMax = GetValueIgnoringNull(dr.GetString(13)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.StandardPartnerDiscountPercentage = GetValueIgnoringNull(dr.GetString(14)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.StandardPartnerMarginPercentage = GetValueIgnoringNull(dr.GetString(15)).Replace(" ", "").Replace("%", "");

                        sp.SmartPriceGuidance.BaseMultiplier = GetValueIgnoringNull(dr.GetString(16)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.LeaseModifier = GetValueIgnoringNull(dr.GetString(17)).Replace(" ", "").Replace("%", "");
                        sp.SmartPriceGuidance.MarginRecommended = GetValueIgnoringNull(dr.GetString(18)).Replace(" ", "").Replace("%", "").Replace("0E-15", "0");
                        sp.SmartPriceGuidance.MarginCompAnchor = GetValueIgnoringNull(dr.GetString(19)).Replace(" ", "").Replace("%", "").Replace("0E-15", "0");
                        sp.SmartPriceGuidance.MarginFloor = GetValueIgnoringNull(dr.GetString(20)).Replace(" ", "").Replace("%", "").Replace("0E-15", "0");
                        sp.SmartPriceGuidance.Quantity = GetValueIgnoringNull(dr.GetString(21));
                        sp.QuoteNumber = GetValueIgnoringNull(dr.GetString(22));
                        sp.SKUNum = GetValueIgnoringNull(dr.GetString(23));
                        sp.SmartPriceID = GetValueIgnoringNull(dr.GetString(24));
                        sp.SmartPriceGuidance.CompmaxAccel = GetValueIgnoringNull(dr.GetString(25));
                        sp.SmartPriceGuidance.Compmaxdecel = GetValueIgnoringNull(dr.GetString(26));
                        sp.SmartPriceGuidance.RebateThreshold = GetValueIgnoringNull(dr.GetString(27));
                        sp.SmartPriceGuidance.IsRebateAvailable = GetValueIgnoringNull(dr.GetString(28));
                        sp.SmartPriceGuidance.RebateType = GetValueIgnoringNull(dr.GetString(29));
                        sp.SFDCDEALID = GetValueIgnoringNull(dr.GetString(30));
                        sp.SFDCUNASSIGNEDENDUSERC = GetValueIgnoringNull(dr.GetString(31));
                        sp.SFDCOPPTYPROBABILITY = GetValueIgnoringNull(dr.GetString(32));
                        sp.SFDCREGISTRATIONSTATUS = GetValueIgnoringNull(dr.GetString(33));
                        sp.SFDCFULFILMENTPATH = GetValueIgnoringNull(dr.GetString(34));
                        sp.SFDCDEALTYPEC = GetValueIgnoringNull(dr.GetString(35));
                        sp.SFDCOPPTYSTAGE = GetValueIgnoringNull(dr.GetString(36));
                        sp.SFDCOPPTYBOOKEDDATE = GetValueIgnoringNull(dr.GetString(37));
                        sp.SFDCDEALREGOPPTY = GetValueIgnoringNull(dr.GetString(38));
                        sp.SFDCOPPTYRECORDTYPE = GetValueIgnoringNull(dr.GetString(39));
                        sp.SFDCDISTIACCOUNTID = GetValueIgnoringNull(dr.GetString(40));
       
                        List< AppliedPolicy > appliedPolicies =AppliedPoliciesJSONDeserializer.GetObject(dr.GetString(41));
                        sp.ServiceType = GetValueIgnoringNull(dr.GetString(42));

                        foreach (AppliedPolicy appliedpolicy in appliedPolicies)
                        {
                            if (appliedpolicy.PolicyName.Replace(" ", "").ToUpper()== "SERVICEMODIFIER-SERVICEMODIFIER")
                            {

                                string[] str = sp.ServiceType.Split(';');
                                for (int i = 0; i < str.Length; i++)
                                {
                                                                        
                                   if(appliedpolicy.PolicyDescription.Contains(str[i]))

                                   { 
                                        appliedpolicy.ServiceType = str[i];
                                        sp.ServiceModifier = appliedpolicy.Value4;

                                                                         
                                    }
                                   

                                }
                                

                            }
                            else
                        {
                            sp.ServiceModifier = "1";
                        }

                            
                            sp.AppliedPolicies = appliedPolicies;
                        }

                        sp.IsDirect= GetValueIgnoringNull(dr.GetString(43));
                        sp.IsPartner = GetValueIgnoringNull(dr.GetString(44));
                        sp.IsRegistered= GetValueIgnoringNull(dr.GetString(45));
                        sp.StandardPartnerDiscount = GetValueIgnoringNull(dr.GetString(46));


                    sp.SmartPriceGuidance.IdentifyFinalRCF(sp.ListPricePerUnit, sp.CostPerUnit);

                    }
                //}

               // con.Close();
            }
            catch (Exception e)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage(e.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage(e.StackTrace);

            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return sp;


        }
       

        public void SmartPriceGuidanceCalculation()
        {
         //   SPLogicObject sp = new SPLogicObject();
         //   SmartPriceCalculationObject spcal = new SmartPriceCalculationObject();
         //   /*G*/
         //   spcal.SpUnitSellingPrice = Convert.ToDouble(sp.ListPricePerUnit) - (Convert.ToDouble(sp.ListPricePerUnit) * (Convert.ToDouble(sp.DiscountedPricePerUnit)/100));//need to change to dsa.discount
         ///*k*/spcal.SpUnitDiscount = Convert.ToDouble(sp.ListPricePerUnit) - spcal.SpUnitSellingPrice;
         // /*a*/spcal.SpRecommendedsellingPrice = Convert.ToDouble(sp.ListPricePerUnit)-(Convert.ToDouble(sp.ListPricePerUnit) * Convert.ToDouble(sp.RecommendedDOLPercentage));
         //  /*B*/spcal.SpCompsellingPrice = Convert.ToDouble(sp.ListPricePerUnit) -(Convert.ToDouble(sp.ListPricePerUnit) * Convert.ToDouble(sp.CompAnchorDOLPercentage));
         //  /*c*/ spcal.SpFloorSellingPrice = Convert.ToDouble(sp.ListPricePerUnit) - (Convert.ToDouble(sp.ListPricePerUnit) * Convert.ToDouble(sp.FloorDOLPercentage));

         //  /*D*/spcal.SpRecommenededSmartPriceRevenue = spcal.SpCompsellingPrice + ((spcal.SpUnitSellingPrice - spcal.SpCompsellingPrice) * (Convert.ToDouble(sp.CompAccel2)));
         //  /*E*/ spcal.SpcompAnchorSmartPriceRevenue = spcal.SpCompsellingPrice + (spcal.SpUnitSellingPrice - spcal.SpCompsellingPrice) * 1;
         //  /*F*/ spcal.SpFloorSmartPriceRevenue = spcal.SpCompsellingPrice + (spcal.SpUnitSellingPrice - spcal.SpCompsellingPrice) * (Convert.ToDouble(sp.CompDecel2));
         //   /*H*/spcal.SpSmartPriceRevenue = spcal.SpCompsellingPrice + (spcal.SpUnitSellingPrice - spcal.SpCompsellingPrice) *Convert.ToDouble(sp.CompAccel2);///  need to recalculate
            
         //   spcal.RecommendedPricingModifier = spcal.SpRecommenededSmartPriceRevenue / spcal.SpRecommendedsellingPrice;
         //   spcal.CompPricingModifier = spcal.SpcompAnchorSmartPriceRevenue / spcal.SpCompsellingPrice;
         //   spcal.floorPricingModifier = spcal.SpFloorSmartPriceRevenue / spcal.SpFloorSellingPrice ;
         //   spcal.PricingModifier = spcal.SpSmartPriceRevenue / spcal.SpUnitSellingPrice;
         //   /*I*/
         //   spcal.Margin = (1 - Convert.ToDouble(sp.CostPerUnit)/ Convert.ToDouble(sp.ListPricePerUnit)) * 100;
         //   /*J*/
         //   spcal.UnitMargin = Convert.ToDouble(sp.ListPricePerUnit) - Convert.ToDouble(sp.CostPerUnit);
         //   /*k1*/
         //   spcal.discount = spcal.SpUnitDiscount * Convert.ToDouble(sp.Quantity);// total discount by value---label change
         //   /*G1*/
         //   spcal.unitsellingPrice = spcal.SpUnitSellingPrice * Convert.ToDouble(sp.Quantity);//total selling price-label change

            //total pricing modifier need to write
            //j1 Margin by value=j*quantity
            // total selling price=sum of  all the product (G1)
            //total margin=sum of I 
           
        }
       
    }
}
