using System;
using System.Collections.Generic;
using System.Text;

namespace IVKeyPro
{

    public class Result
    {
        const int RANK_X = 16;
        const double CONDITION_RANK_X = 101;

        const int RANK_SSplus = 15;
        const double CONDITION_RANK_SSplus = 100.95;

        const int RANK_SS = 14;
        const double CONDITION_RANK_SS = 100.9;

        const int RANK_Splus = 13;
        const double CONDITION_RANK_Splus = 100.75;

        const int RANK_S = 12;
        const double CONDITION_RANK_S = 100;

        const int RANK_AAplus_fc = 11;
        const int RANK_AA_fc = 9;
        const int RANK_Aplus_fc = 7;
        const int RANK_A_fc = 5;

        const int RANK_AAplus = 10;
        const double CONDITION_RANK_AAplus = 99;

        const int RANK_AA = 8;
        const double CONDITION_RANK_AA = 97;

        const int RANK_Aplus = 6;
        const double CONDITION_RANK_Aplus = 95;

        const int RANK_A = 4;
        const double CONDITION_RANK_A = 93;

        const int RANK_Bplus = 3;
        const double CONDITION_RANK_Bplus = 90;

        const int RANK_B = 2;
        const double CONDITION_RANK_B = 85;

        const int RANK_C = 1;
        const double CONDITION_RANK_C = 75;

        const int RANK_D = 0;


        public int proplus = 0;
        public int pro = 0;
        public int noob = 0;
        public int bad = 0;
        public int miss = 0;
        public int ID = 0;
        public double score = 0;
        public double acc = 0;
        public int maxcombo = 0;
        public int subscore = 0;
        public int rank = -1;
        public Result()
        {

        }
        public Result(int ID)
        {
            this.ID = ID;
        }
        public int GetRank()
        {
            WhatisMyRank();
            return rank;
        }
        public void WhatisMyRank()
        {
            if (acc == CONDITION_RANK_X)
                rank = RANK_X;

            else if (CONDITION_RANK_SSplus <= acc)
                rank = RANK_SSplus;

            else if (CONDITION_RANK_SS <= acc)
                rank = RANK_SS;

            else if (CONDITION_RANK_Splus <= acc)
                rank = RANK_Splus;

            else if (CONDITION_RANK_S <= acc)
                rank = RANK_S;

            else if (CONDITION_RANK_AAplus <= acc)
            {
                rank = RANK_AAplus;
                if (miss == 0) rank = RANK_AAplus_fc;
            }
            else if (CONDITION_RANK_AA <= acc)
            {
                rank = RANK_AA;
                if (miss == 0) rank = RANK_AA_fc;
            }
            else if (CONDITION_RANK_Aplus <= acc)
            {
                rank = RANK_Aplus;
                if (miss == 0) rank = RANK_Aplus_fc;
            }
            else if (CONDITION_RANK_A <= acc)
            {
                rank = RANK_A;
                if (miss == 0) rank = RANK_A_fc;
            }
            else if (CONDITION_RANK_Bplus <= acc)
                rank = RANK_Bplus;

            else if (CONDITION_RANK_B <= acc)
                rank = RANK_B;

            else if (CONDITION_RANK_C <= acc)
                rank = RANK_C;

            else
                rank = RANK_D;


        }
        public Result(int proplus, int pro,int noob,int bad,int miss,int ID,double score,double acc,int rank)
        {
            this.proplus = proplus;
            this.pro = pro;
            this.noob = noob;
            this.bad = bad;
            this.miss = miss;
            this.ID = ID;
            this.score = score;
            this.acc = acc;
            this.rank = rank;
        }
    }
}
