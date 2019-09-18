using System;
using System.Collections.Generic;

namespace bamboohr_jobtest
{
    public class Member 
    {
        private string name;
        private int seniority;
        private Member boss;
        private List<Member> subordinates;

        public string Name { get => name; set => name = value; }
        public int Seniority { get => seniority; set => seniority = value; }
        public Member Boss { get => boss; set => boss = value; }
        public List<Member> Subordinates { get => subordinates; set => subordinates = value; }

        public Member()
        {
            Name = String.Empty;
            Seniority = 0;
            Boss = null;
            Subordinates = new List<Member>();
        }

        public Member(string _name, int _seniority = 0, Member _boss = null, List<Member> _subordinates = null)
        {
            Name = _name;
            Seniority = _seniority;
            Boss = _boss;

            if(_subordinates != null)
            {
                Subordinates = new List<Member>(_subordinates);
            }
            else 
            {
                Subordinates = new List<Member>();
            }
        }

        public Member(Member member)
        {
            Name = member.Name;
            Seniority = member.Seniority;
            Boss = member.Boss;
            Subordinates = new List<Member>(member.Subordinates);
        }
    }
}