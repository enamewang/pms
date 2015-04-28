using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Qisda.PMS.Business
{
    public class PmsCommonEnum
    {
        //add by Abel on 2014-01-16
        public enum TaskComplexity
        {
            [Description("High")]
            High = 1,

            [Description("Middle")]
            Middle = 2,

            [Description("Low")]
            Low = 3
        }     

        public enum OperationType
        {
            [Description("全新功能/全新逻辑")]
            New = 1,

            [Description("修改原功能/原逻辑")]
            Edit = 2
        }

        public enum ProgramLanguage
        {
            [Description("VB")]
            VB = 1,

            [Description("C#")]
            CShape = 2,

            [Description("Other")]
            Other = 3
        }

        public enum FunctionType
        {
            [Description("Inquiry")]
            Inquiry = 1,

            [Description("Report")]
            Report = 2,

            [Description("Maintain")]
            Maintain = 3,

            [Description("Process")]
            Process = 4,

            [Description("Job")]
            Job = 5,

            [Description("Other")]
            Other = 6

        }

        //end add

        public enum PlanPhase
        {
            [Description("设计阶段")]
            Design = 4,

            [Description("开发阶段")]
            Development = 5,

            [Description("测试阶段")]
            Test = 6,

            [Description("Release阶段")]
            Release = 7,

            [Description("Support阶段")]
            Support = 8,

            [Description("PES阶段")]
            PES = 9,

            [Description("上线实施阶段")]
            OnLine = 10
        }

        public enum AuditStatus
        {
            [Description("未提交")]
            NotSubmit = 1,

            [Description("待审核")]
            WaitApprove = 2,

            [Description("已批准")]
            HasApproved = 3,

            [Description("已拒绝")]
            BeenRejected = 4
        }

        public enum TaskStatus
        {
            [Description("未开始")]
            NotStart = 1,

            [Description("进行中")]
            OnGoing = 2,

            [Description("已完成")]
            Completed = 3,

            [Description("已关闭")]
            Closed = 4,

            [Description("已取消")]
            Cancelled = 5,

            [Description("已暂缓")]
            Pending = 6
        }

        public enum EnumSdpPhase
        {
            [Description("Design")]
            Design = 4,

            [Description("Development")]
            Development = 5,

            [Description("Test")]
            Test = 6,

            [Description("Release")]
            Release = 7,

            [Description("Support")]
            Support = 8
        }

        public enum Role
        {
            [Description("PM")]
            PM,
            [Description("SD")]
            SD,
            [Description("SE")]
            SE,
            [Description("SE")]
            QA
        }

        public enum OrgRole
        {
            [Description("PM")]
            PM = 1,
            [Description("RD")]
            RD = 2,
            [Description("RD LEADER")]
            RD_LEADER = 3,
            [Description("RD MANAGER")]
            RD_MANAGER = 4,
            [Description("PM MANAGER")]
            PM_MANAGER = 5
        }

        public enum SdpStage
        {
            Release = 7,
            Support = 8,
            Pending = 10,
            Cancelled = 11,
            HardClosed = 12
        }

        //所有的项目Stage
        [Description("Project Stage")]
        public enum ProjectStage
        {
            [Description("Waiting Approve")]
            WaitingApprove = 1,

            [Description("Assign Member")]
            AssignMember = 2,

            [Description("PES")]
            PES = 3,

            [Description("PIS|STP")]
            PIS_STP = 4,

            [Description("Develop|Test")]
            Develop_Test = 5,

            [Description("Release")]
            Release = 6,

            [Description("Closed")]
            Closed = 7,

            [Description("Pending")]
            Pending = 8,

            [Description("Cancelled")]
            Cancelled = 9,

            [Description("HardClosed")]
            HardClosed = 10,

            [Description("Waiting Closed")]
            WaitingClosed = 11,

            [Description("Reactive")]
            Reactive = 12
        }

        public enum ProjectTypeFlowId
        {
            [Description("CR")]
            CR = 1,

            [Description("Project")]
            Project = 4,

            [Description("Small CR")]
            SmallCR = 5,

            [Description("Study")]
            Study = 6,

            [Description("Service")]
            Service = 7,
        }

        public enum MeetingType
        {
            [Description("PES Meeting")]
            PESMeeting = 1,

            [Description("PIS Meeting")]
            PISMeeting = 2,

            [Description("STP Meeting")]
            STPMeeting = 3,

            [Description("STC Meeting")]
            STCMeeting = 4,

            [Description("RLN Meeting")]
            RLNMeeting = 5,

            [Description("Other")]
            Other = 6,
        }


        //public enum MailType
        //{
        //    Create = 1,
        //    Promote = 2
        //}
        public enum MailType
        {
            CreateMail = 1,
            AssignMemberMail = 2,
            PISAndSTPMail = 3,
            DevelopAndTestMail = 4,
            ReleaseMail = 5,
            ClosedMail = 6,
            HardClosedMail = 7,
            PendingMail = 8,
            CancelledMail = 9,
            ReactiveMail = 10,
            ConfirmMail = 13,
            InformationChangedMail = 12,
            WaitingClosed = 11,
            CreateMeetingMinuteMail = 14,
            EditMeetingMinuteMail = 15
        }

        public enum EnumTime
        {
            Past = 1,
            Today = 2,
            Future = 3

        }

        public enum DocumentType
        {
            [Description("PES")]
            PES = 1,

            [Description("PIS")]
            PIS = 2,

            [Description("STP")]
            STP = 3,

            [Description("STC")]
            STC = 4,

            [Description("RLN")]
            RLN = 5,

            [Description("Study Report")]
            Study_Report = 6,

            [Description("Other")]
            Other = 7,

            [Description("PES MIN")]
            PES_MIN = 8,

            [Description("PIS MIN")]
            PIS_MIN = 9,

            [Description("STP MIN")]
            STP_MIN = 10,

            [Description("RLN MIN")]
            RLN_MIN = 11
        }

        //add by Abel li  2014-01-07
        /// <summary>
        /// 获取enum的 value 和 description
        /// (用法：在DataSource赋值之后，DataTextField = "Key"，DataValueField = "Value")
        /// </summary>
        /// <param name="enumType">传入待获取的enum类型，例如:typeof(JudgementMode)</param>
        /// <returns>返回Dictionary(description,Value)</returns>
        public Dictionary<string, string> GetEnumValueAndDesc(Type enumType)
        {
            // set enum Name 
            Type enumName = enumType;
            Dictionary<string, string> statusDictionary = new Dictionary<string, string>();
            if (enumName == null)
            {
                return statusDictionary;
            }
            // get enum fileds
            FieldInfo[] fields = enumName.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (!field.FieldType.IsEnum)
                {
                    continue;
                }
                // get enum value
                int value = (int)enumName.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                string description = string.Empty;
                object[] array = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (array.Length > 0)
                {
                    description = ((DescriptionAttribute)array[0]).Description;
                }
                else
                {
                    description = ""; //none description,set empty
                }
                //add to Dictionary
                statusDictionary.Add(description, value.ToString());
            }
            return statusDictionary ?? new Dictionary<string, string>();
        }
    }
}
