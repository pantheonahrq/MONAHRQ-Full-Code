
/*drop table [NH_Base]
drop table [NH_Provider_Base]
drop table [NH_Rates_All]
drop table nh_responsedetail*/

if (Object_id(N'NH CAHPS Sample_working'))		is not null drop table [NH CAHPS Sample_working]
if (Object_id(N'NH CAHPS Sample_working2'))		is not null drop table [NH CAHPS Sample_working2]
if (Object_id(N'NH CAHPS Sample_working3'))		is not null drop table [NH CAHPS Sample_working3]
if (Object_id(N'NH CAHPS Sample_FinalComp'))	is not null drop table [NH CAHPS Sample_FinalComp]
if (Object_id(N'NH CAHPS Sample_Final'))		is not null drop table [NH CAHPS Sample_Final]
if (Object_id(N'NH_Peer'))						is not null drop table [NH_Peer]



if (Object_id(N'NH_Base'))					is not null drop table NH_Base
if (Object_id(N'NH_Provider_Base'))			is not null drop table NH_Provider_Base
if (Object_id(N'NH_Rates_All'))				is not null drop table NH_Rates_All
if (Object_id(N'nh_responsedetail'))		is not null drop table nh_responsedetail

