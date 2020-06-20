using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.RabbitMqHelper
{
    public class QueueResponse
    {
        public class Rootobject
        {
            public Consumer_Details[] consumer_details { get; set; }
            public Arguments arguments { get; set; }
            public bool? auto_delete { get; set; }
            public Backing_Queue_Status backing_queue_status { get; set; }
            public float? consumer_utilisation { get; set; }
            public long? consumers { get; set; }
            public object[] deliveries { get; set; }
            public bool? durable { get; set; }
            public Effective_Policy_Definition effective_policy_definition { get; set; }
            public bool? exclusive { get; set; }
            public object exclusive_consumer_tag { get; set; }
            public Garbage_Collection garbage_collection { get; set; }
            public object head_message_timestamp { get; set; }
            public object[] incoming { get; set; }
            public long? memory { get; set; }
            public long? message_bytes { get; set; }
            public long? message_bytes_paged_out { get; set; }
            public long? message_bytes_persistent { get; set; }
            public long? message_bytes_ram { get; set; }
            public long? message_bytes_ready { get; set; }
            public long? message_bytes_unacknowledged { get; set; }
            public Message_Stats message_stats { get; set; }
            public long? messages { get; set; }
            public Messages_Details messages_details { get; set; }
            public long? messages_paged_out { get; set; }
            public long? messages_persistent { get; set; }
            public long? messages_ram { get; set; }
            public long? messages_ready { get; set; }
            public Messages_Ready_Details messages_ready_details { get; set; }
            public long? messages_ready_ram { get; set; }
            public long? messages_unacknowledged { get; set; }
            public Messages_Unacknowledged_Details messages_unacknowledged_details { get; set; }
            public long? messages_unacknowledged_ram { get; set; }
            public string name { get; set; }
            public string node { get; set; }
            public object operator_policy { get; set; }
            public object policy { get; set; }
            public object recoverable_slaves { get; set; }
            public long? reductions { get; set; }
            public Reductions_Details reductions_details { get; set; }
            public object single_active_consumer_tag { get; set; }
            public string state { get; set; }
            public string type { get; set; }
            public string vhost { get; set; }
        }

        public class Arguments
        {
            public long? xmaxpriority { get; set; }
        }

        public class Backing_Queue_Status
        {
            public float? avg_ack_egress_rate { get; set; }
            public float? avg_ack_ingress_rate { get; set; }
            public float? avg_egress_rate { get; set; }
            public float? avg_ingress_rate { get; set; }
            public string[] delta { get; set; }
            public long? len { get; set; }
            public string mode { get; set; }
            public long? next_seq_id { get; set; }
            public Priority_Lengths priority_lengths { get; set; }
            public long? q1 { get; set; }
            public long? q2 { get; set; }
            public long? q3 { get; set; }
            public long? q4 { get; set; }
            public string target_ram_count { get; set; }
        }

        public class Priority_Lengths
        {
            public long? _0 { get; set; }
            public long? _1 { get; set; }
            public long? _2 { get; set; }
            public long? _3 { get; set; }
            public long? _4 { get; set; }
            public long? _5 { get; set; }
            public long? _6 { get; set; }
            public long? _7 { get; set; }
            public long? _8 { get; set; }
            public long? _9 { get; set; }
            public long? _10 { get; set; }
            public long? _11 { get; set; }
            public long? _12 { get; set; }
            public long? _13 { get; set; }
            public long? _14 { get; set; }
            public long? _15 { get; set; }
            public long? _16 { get; set; }
            public long? _17 { get; set; }
            public long? _18 { get; set; }
            public long? _19 { get; set; }
            public long? _20 { get; set; }
            public long? _21 { get; set; }
            public long? _22 { get; set; }
            public long? _23 { get; set; }
            public long? _24 { get; set; }
            public long? _25 { get; set; }
            public long? _26 { get; set; }
            public long? _27 { get; set; }
            public long? _28 { get; set; }
            public long? _29 { get; set; }
            public long? _30 { get; set; }
            public long? _31 { get; set; }
            public long? _32 { get; set; }
            public long? _33 { get; set; }
            public long? _34 { get; set; }
            public long? _35 { get; set; }
            public long? _36 { get; set; }
            public long? _37 { get; set; }
            public long? _38 { get; set; }
            public long? _39 { get; set; }
            public long? _40 { get; set; }
            public long? _41 { get; set; }
            public long? _42 { get; set; }
            public long? _43 { get; set; }
            public long? _44 { get; set; }
            public long? _45 { get; set; }
            public long? _46 { get; set; }
            public long? _47 { get; set; }
            public long? _48 { get; set; }
            public long? _49 { get; set; }
            public long? _50 { get; set; }
            public long? _51 { get; set; }
            public long? _52 { get; set; }
            public long? _53 { get; set; }
            public long? _54 { get; set; }
            public long? _55 { get; set; }
            public long? _56 { get; set; }
            public long? _57 { get; set; }
            public long? _58 { get; set; }
            public long? _59 { get; set; }
            public long? _60 { get; set; }
            public long? _61 { get; set; }
            public long? _62 { get; set; }
            public long? _63 { get; set; }
            public long? _64 { get; set; }
            public long? _65 { get; set; }
            public long? _66 { get; set; }
            public long? _67 { get; set; }
            public long? _68 { get; set; }
            public long? _69 { get; set; }
            public long? _70 { get; set; }
            public long? _71 { get; set; }
            public long? _72 { get; set; }
            public long? _73 { get; set; }
            public long? _74 { get; set; }
            public long? _75 { get; set; }
            public long? _76 { get; set; }
            public long? _77 { get; set; }
            public long? _78 { get; set; }
            public long? _79 { get; set; }
            public long? _80 { get; set; }
            public long? _81 { get; set; }
            public long? _82 { get; set; }
            public long? _83 { get; set; }
            public long? _84 { get; set; }
            public long? _85 { get; set; }
            public long? _86 { get; set; }
            public long? _87 { get; set; }
            public long? _88 { get; set; }
            public long? _89 { get; set; }
            public long? _90 { get; set; }
            public long? _91 { get; set; }
            public long? _92 { get; set; }
            public long? _93 { get; set; }
            public long? _94 { get; set; }
            public long? _95 { get; set; }
            public long? _96 { get; set; }
            public long? _97 { get; set; }
            public long? _98 { get; set; }
            public long? _99 { get; set; }
            public long? _100 { get; set; }
            public long? _101 { get; set; }
            public long? _102 { get; set; }
            public long? _103 { get; set; }
            public long? _104 { get; set; }
            public long? _105 { get; set; }
            public long? _106 { get; set; }
            public long? _107 { get; set; }
            public long? _108 { get; set; }
            public long? _109 { get; set; }
            public long? _110 { get; set; }
            public long? _111 { get; set; }
            public long? _112 { get; set; }
            public long? _113 { get; set; }
            public long? _114 { get; set; }
            public long? _115 { get; set; }
            public long? _116 { get; set; }
            public long? _117 { get; set; }
            public long? _118 { get; set; }
            public long? _119 { get; set; }
            public long? _120 { get; set; }
            public long? _121 { get; set; }
            public long? _122 { get; set; }
            public long? _123 { get; set; }
            public long? _124 { get; set; }
            public long? _125 { get; set; }
            public long? _126 { get; set; }
            public long? _127 { get; set; }
            public long? _128 { get; set; }
            public long? _129 { get; set; }
            public long? _130 { get; set; }
            public long? _131 { get; set; }
            public long? _132 { get; set; }
            public long? _133 { get; set; }
            public long? _134 { get; set; }
            public long? _135 { get; set; }
            public long? _136 { get; set; }
            public long? _137 { get; set; }
            public long? _138 { get; set; }
            public long? _139 { get; set; }
            public long? _140 { get; set; }
            public long? _141 { get; set; }
            public long? _142 { get; set; }
            public long? _143 { get; set; }
            public long? _144 { get; set; }
            public long? _145 { get; set; }
            public long? _146 { get; set; }
            public long? _147 { get; set; }
            public long? _148 { get; set; }
            public long? _149 { get; set; }
            public long? _150 { get; set; }
            public long? _151 { get; set; }
            public long? _152 { get; set; }
            public long? _153 { get; set; }
            public long? _154 { get; set; }
            public long? _155 { get; set; }
            public long? _156 { get; set; }
            public long? _157 { get; set; }
            public long? _158 { get; set; }
            public long? _159 { get; set; }
            public long? _160 { get; set; }
            public long? _161 { get; set; }
            public long? _162 { get; set; }
            public long? _163 { get; set; }
            public long? _164 { get; set; }
            public long? _165 { get; set; }
            public long? _166 { get; set; }
            public long? _167 { get; set; }
            public long? _168 { get; set; }
            public long? _169 { get; set; }
            public long? _170 { get; set; }
            public long? _171 { get; set; }
            public long? _172 { get; set; }
            public long? _173 { get; set; }
            public long? _174 { get; set; }
            public long? _175 { get; set; }
            public long? _176 { get; set; }
            public long? _177 { get; set; }
            public long? _178 { get; set; }
            public long? _179 { get; set; }
            public long? _180 { get; set; }
            public long? _181 { get; set; }
            public long? _182 { get; set; }
            public long? _183 { get; set; }
            public long? _184 { get; set; }
            public long? _185 { get; set; }
            public long? _186 { get; set; }
            public long? _187 { get; set; }
            public long? _188 { get; set; }
            public long? _189 { get; set; }
            public long? _190 { get; set; }
            public long? _191 { get; set; }
            public long? _192 { get; set; }
            public long? _193 { get; set; }
            public long? _194 { get; set; }
            public long? _195 { get; set; }
            public long? _196 { get; set; }
            public long? _197 { get; set; }
            public long? _198 { get; set; }
            public long? _199 { get; set; }
            public long? _200 { get; set; }
            public long? _201 { get; set; }
            public long? _202 { get; set; }
            public long? _203 { get; set; }
            public long? _204 { get; set; }
            public long? _205 { get; set; }
            public long? _206 { get; set; }
            public long? _207 { get; set; }
            public long? _208 { get; set; }
            public long? _209 { get; set; }
            public long? _210 { get; set; }
            public long? _211 { get; set; }
            public long? _212 { get; set; }
            public long? _213 { get; set; }
            public long? _214 { get; set; }
            public long? _215 { get; set; }
            public long? _216 { get; set; }
            public long? _217 { get; set; }
            public long? _218 { get; set; }
            public long? _219 { get; set; }
            public long? _220 { get; set; }
            public long? _221 { get; set; }
            public long? _222 { get; set; }
            public long? _223 { get; set; }
            public long? _224 { get; set; }
            public long? _225 { get; set; }
            public long? _226 { get; set; }
            public long? _227 { get; set; }
            public long? _228 { get; set; }
            public long? _229 { get; set; }
            public long? _230 { get; set; }
            public long? _231 { get; set; }
            public long? _232 { get; set; }
            public long? _233 { get; set; }
            public long? _234 { get; set; }
            public long? _235 { get; set; }
            public long? _236 { get; set; }
            public long? _237 { get; set; }
            public long? _238 { get; set; }
            public long? _239 { get; set; }
            public long? _240 { get; set; }
            public long? _241 { get; set; }
            public long? _242 { get; set; }
            public long? _243 { get; set; }
            public long? _244 { get; set; }
            public long? _245 { get; set; }
            public long? _246 { get; set; }
            public long? _247 { get; set; }
            public long? _248 { get; set; }
            public long? _249 { get; set; }
            public long? _250 { get; set; }
            public long? _251 { get; set; }
            public long? _252 { get; set; }
            public long? _253 { get; set; }
            public long? _254 { get; set; }
            public long? _255 { get; set; }
        }

        public class Effective_Policy_Definition
        {
        }

        public class Garbage_Collection
        {
            public long? fullsweep_after { get; set; }
            public long? max_heap_size { get; set; }
            public long? min_bin_vheap_size { get; set; }
            public long? min_heap_size { get; set; }
            public long? minor_gcs { get; set; }
        }

        public class Message_Stats
        {
            public long? ack { get; set; }
            public Ack_Details ack_details { get; set; }
            public long? deliver { get; set; }
            public Deliver_Details deliver_details { get; set; }
            public long? deliver_get { get; set; }
            public Deliver_Get_Details deliver_get_details { get; set; }
            public long? deliver_no_ack { get; set; }
            public Deliver_No_Ack_Details deliver_no_ack_details { get; set; }
            public long? get { get; set; }
            public Get_Details get_details { get; set; }
            public long? get_empty { get; set; }
            public Get_Empty_Details get_empty_details { get; set; }
            public long? get_no_ack { get; set; }
            public Get_No_Ack_Details get_no_ack_details { get; set; }
            public long? publish { get; set; }
            public Publish_Details publish_details { get; set; }
            public long? redeliver { get; set; }
            public Redeliver_Details redeliver_details { get; set; }
        }

        public class Ack_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample[] samples { get; set; }
        }

        public class Sample
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Deliver_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample1[] samples { get; set; }
        }

        public class Sample1
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Deliver_Get_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample2[] samples { get; set; }
        }

        public class Sample2
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Deliver_No_Ack_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample3[] samples { get; set; }
        }

        public class Sample3
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Get_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample4[] samples { get; set; }
        }

        public class Sample4
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Get_Empty_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample5[] samples { get; set; }
        }

        public class Sample5
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Get_No_Ack_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample6[] samples { get; set; }
        }

        public class Sample6
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Publish_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample7[] samples { get; set; }
        }

        public class Sample7
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Redeliver_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample8[] samples { get; set; }
        }

        public class Sample8
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Messages_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample9[] samples { get; set; }
        }

        public class Sample9
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Messages_Ready_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample10[] samples { get; set; }
        }

        public class Sample10
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Messages_Unacknowledged_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample11[] samples { get; set; }
        }

        public class Sample11
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Reductions_Details
        {
            public float? avg { get; set; }
            public float? avg_rate { get; set; }
            public float? rate { get; set; }
            public Sample12[] samples { get; set; }
        }

        public class Sample12
        {
            public long? sample { get; set; }
            public long? timestamp { get; set; }
        }

        public class Consumer_Details
        {
            public Arguments1 arguments { get; set; }
            public Channel_Details channel_details { get; set; }
            public bool? ack_required { get; set; }
            public bool? active { get; set; }
            public string activity_status { get; set; }
            public string consumer_tag { get; set; }
            public bool? exclusive { get; set; }
            public long? prefetch_count { get; set; }
            public Queue queue { get; set; }
        }

        public class Arguments1
        {
        }

        public class Channel_Details
        {
            public string connection_name { get; set; }
            public string name { get; set; }
            public string node { get; set; }
            public long? number { get; set; }
            public string peer_host { get; set; }
            public long? peer_port { get; set; }
            public string user { get; set; }
        }

        public class Queue
        {
            public string name { get; set; }
            public string vhost { get; set; }
        }

    }
}
