using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;


namespace Qisda.PMS.Business
{
    public class ProjectProgressBiz : BaseBusiness
    {
        /// <summary>
        /// 检查是否已上传stage必要的文档
        /// </summary>
        /// <param name="pmsId">当前的PmsId</param>
        /// <param name="stage">当前的项目进度Stage</param>
        /// <param name="message">记录用以返回的必要的提示信息</param>
        /// <returns>如果该Stage必要的所有文档都已上传，则返回true，否则返回false</returns>
        public bool CheckDocuments(PmsHead pmsHead, int stage, out string message)
        {
            message = string.Empty;

            // modified by Ename Wang on 20120209
            string pmsId = pmsHead.PmsId;
            PmsDocumentTypeBiz pmsDocumentTypeBiz = new PmsDocumentTypeBiz();
            IList<PmsDocumentType> pmsDocumentTypeIdName = pmsDocumentTypeBiz.SelectDistinctDocTypeIdName(pmsId, stage);
            //end modified

            //STP属于PIS|STP阶段的产出文档，当由Develop|Test Premote到下一个Stage时要做额外的check。
            //STC属于Develop|Test 阶段的产出文档，当由Develop|Test  Stage Premote到下一个Stage时要做额外的check。
            if (stage == (int)PmsCommonEnum.ProjectStage.Develop_Test)
            {
                if (pmsHead.NeedSTP == "Y")
                {
                    PmsDocumentType pmsDocumentTypeSTP = new PmsDocumentType();
                    pmsDocumentTypeSTP.TypeId = (int)PmsCommonEnum.DocumentType.STP;
                    pmsDocumentTypeSTP.TypeName = PmsCommonEnum.DocumentType.STP.GetDescription();
                    pmsDocumentTypeIdName.Add(pmsDocumentTypeSTP);
                }

                if (pmsHead.NeedSTC == "Y")
                {
                    PmsDocumentType pmsDocumentTypeSTC = new PmsDocumentType();
                    pmsDocumentTypeSTC.TypeId = (int)PmsCommonEnum.DocumentType.STC;
                    pmsDocumentTypeSTC.TypeName = PmsCommonEnum.DocumentType.STC.GetDescription();
                    pmsDocumentTypeIdName.Add(pmsDocumentTypeSTC);
                }
            }

            // add by Ename Wang on 20130308  在点Release or Partial Release的时候，如果开发计划工时>80H 就必须上传STP,STC
            if (stage == (int)PmsCommonEnum.ProjectStage.Release)
            {
                SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
                SdpDetail sdpDetail = new SdpDetail();
                sdpDetail.Pmsid = pmsHead.PmsId;
                sdpDetail.Phase = "5";
                IList<SdpDetail> sdpDetailList = sdpDetailBiz.SelectSdpDetail(sdpDetail);
                decimal totalPlanCost = (decimal)sdpDetailList.Select(t => t.Plancost).Sum();
                if (totalPlanCost > 80)
                {
                    PmsDocumentType pmsDocumentTypeSTP = new PmsDocumentType();
                    pmsDocumentTypeSTP.TypeId = (int)PmsCommonEnum.DocumentType.STP;
                    pmsDocumentTypeSTP.TypeName = PmsCommonEnum.DocumentType.STP.GetDescription();
                    pmsDocumentTypeIdName.Add(pmsDocumentTypeSTP);

                    PmsDocumentType pmsDocumentTypeSTC = new PmsDocumentType();
                    pmsDocumentTypeSTC.TypeId = (int)PmsCommonEnum.DocumentType.STC;
                    pmsDocumentTypeSTC.TypeName = PmsCommonEnum.DocumentType.STC.GetDescription();
                    pmsDocumentTypeIdName.Add(pmsDocumentTypeSTC);
                }
            }
            // end add

            //TypeId为int型，所以需要不用加上‘’
            string docTypeId = pmsDocumentTypeIdName.Aggregate(string.Empty, (current, pmsDocumentType) => +pmsDocumentType.TypeId + "," + current);

            //如果docTypeId为空，则证明该stage不需要文档，因此返回true
            if (string.IsNullOrEmpty(docTypeId))
            {
                message = string.Empty;
                return true;
            }

            //如果docTypeId不为空，则需要检查数据库中是否存在该stage必要上传的文件。
            docTypeId = docTypeId.Substring(0, docTypeId.Length - 1);
            PmsDocumentsBiz pmsDocumentsBiz = new PmsDocumentsBiz();
            IList<PmsDocuments> pmsDocumentsDocTypeId = pmsDocumentsBiz.SelectPmsDocumentsDocTypeId(pmsHead.PmsId, docTypeId);
            IList<int> typeIdExist = pmsDocumentsDocTypeId.Select(t => t.DocTypeId).ToList();
            // IList<int> typeIdExist = pmsDocumentsBiz.SelectPmsDocumentsDocTypeId(pmsId, docTypeId);
            bool result = true;
            StringBuilder msg = new StringBuilder();
            msg.Append("Please upload the necessary documents:");
            foreach (PmsDocumentType documentType in pmsDocumentTypeIdName)
            {
                if (!typeIdExist.Contains(documentType.TypeId))
                {
                    msg.Append(documentType.TypeName);
                    msg.Append(",");
                    result = false;
                }

            }
            if (!result)
            {
                message = msg.ToString();
                message = message.Substring(0, message.Length - 1) + "!";
            }

            return result;
        }

        public IList<string> SelectSdpDetailTemplatePhase(string type)
        {
            try
            {

                return m_PMSSqlConnection.QueryForList<string>("SelectSdpDetailTemplatePhase", type);

            }
            catch (Exception ex)
            {
                m_Logger.Error("ProjectProgressBiz/SelectSdpDetailTemplatePhase:" + ex.ToString());
                return null;
            }
        }

    }
}
