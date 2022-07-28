using System;
using System.Collections.Generic;
using System.Threading;

namespace LiveBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var links = new string[]
            {
                "https://www.bezrealitky.cz/vyhledat?offerType=PRONAJEM&estateType=BYT&disposition=DISP_3_1%2FDISP_4_KK%2FDISP_4_1%2FDISP_5_KK%2FDISP_5_1%2FDISP_6_KK%2FDISP_6_1%2FDISP_7_KK%2FDISP_7_1&priceFrom=1&priceTo=22000&order=TIMEORDER_DESC&regionOsmId=R435541&osm_value=Praha%2C+Česko&page=1",
                "https://www.sreality.cz/api/cs/v2/estates?category_main_cb=1&category_sub_cb=7%7C8%7C9%7C10%7C11%7C12&category_type_cb=2&czk_price_summary_order2=0%7C22000&locality_region_id=10&per_page=20&tms=1658931611039",
                "https://reality.idnes.cz/s/pronajem/byty/do-22000-za-mesic/praha/?s-qc%5BsubtypeFlat%5D%5B0%5D=31&s-qc%5BsubtypeFlat%5D%5B1%5D=4k&s-qc%5BsubtypeFlat%5D%5B2%5D=41&s-qc%5BsubtypeFlat%5D%5B3%5D=5k&s-qc%5BsubtypeFlat%5D%5B4%5D=51&s-qc%5BsubtypeFlat%5D%5B5%5D=6k",
                "https://www.ceskereality.cz/pronajem/byty/byty-3-1/kraj-hlavni-mesto-praha/do-22000/nejnovejsi/?d_subtyp=208,209,210,211",
                "https://realitymix.cz/vypis-nabidek/?form%5Badresa_kraj_id%5D[]=19&form%5Bcena_mena%5D=&form%5Bcena_normalizovana__from%5D=&form%5Bcena_normalizovana__to%5D=22000&form%5Bdispozice%5D[]=11&form%5Bdispozice%5D[]=5&form%5Bdispozice%5D[]=12&form%5Bdispozice%5D[]=6&form%5Bdispozice%5D[]=13&form%5Bdispozice%5D[]=7&form%5Bdispozice%5D[]=14&form%5Bdispozice%5D[]=8&form%5Bdispozice%5D[]=15&form%5Bexclusive%5D=&form%5Bfk_rk%5D=&form%5Binzerat_typ%5D=2&form%5Bnemovitost_typ%5D[]=4&form%5Bplocha__from%5D=&form%5Bplocha__to%5D=&form%5Bpodlazi_cislo__from%5D=&form%5Bpodlazi_cislo__to%5D=&form%5Bprojekt_id%5D=&form%5Bsearch_in_city%5D=&form%5Bsearch_in_text%5D=&form%5Bstari_inzeratu%5D=&form%5Bstav_objektu%5D=&form%5Btop_nabidky%5D=",
                "https://www.remax-czech.cz/reality/vyhledavani/?price_to=22000&regions%5B19%5D=on&types%5B4%5D%5B5%5D=on&types%5B4%5D%5B6%5D=on&types%5B4%5D%5B7%5D=on&types%5B4%5D%5B8%5D=on&types%5B4%5D%5B11%5D=on&types%5B4%5D%5B12%5D=on&types%5B4%5D%5B13%5D=on&types%5B4%5D%5B14%5D=on&types%5B4%5D%5B15%5D=on"
            };
            var values = new List<Values>();
            var emailer = new Emailer();
            for (var i = 0; i < links.Length; i++)
            {
                values.Add(new Values(i, links));
            }
            emailer.Loop(values, links);
        }
        
    }
}