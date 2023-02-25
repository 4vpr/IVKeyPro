using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IVKeyPro
{
    public partial class InGame
    {
        public Result gameResult;
        public double subMaxscore = 0;




        public void DoLongNote()
        //롱 노트 구현
        {
            for (int i = 0; i < 4; i++)
            {

                if (isLongNote[i])
                    // 다음 노트가 롱노트라면
                {
                    acc_long[i] = nextnote[i].timing + nextnote[i].length - songms;
                    
                    if (!_keyChecker.keyDown(Setting.keySetting[i]))
                    {
                        if (acc_long[i] < 50 && acc_long[i] > -50)
                        {
                            CheckLongNote(i);
                            isLongNote[i] = false;
                            Judgement(1);
                        }
                        else if (acc_long[i] < 150 && acc_long[i] > -150)
                        {
                            CheckLongNote(i);
                            isLongNote[i] = false;
                            Judgement(2);

                        }
                        else if (acc_long[i] < 250 && acc_long[i] > -250)
                        {
                            CheckLongNote(i);
                            isLongNote[i] = false;
                            Judgement(3);

                        }
                        else if (acc_long[i] < 350)
                        {
                            CheckLongNote(i);
                            isLongNote[i] = false;
                            Judgement(4);

                        }
                        else
                        {
                            FailLongNote(nextnote[i]);
                            CheckLongNote(i);
                            isLongNote[i] = false;
                            //Console.WriteLine("reason 3");
                            Judgement(0);
                        }
                    }
                    if (acc_long[i] < -350)
                    {
                        FailLongNote(nextnote[i]);
                        UpdateLine(nextnote[i]);
                        isLongNote[i] = false;
                        //Console.WriteLine("reason 4");
                        Judgement(4);
                    }
                }
            }
        }
        public void FailLongNote(Note note)
        {
            failedLongNote.Add(note);
        }
        public void UpdateLine(Note prevnote)
        {
            spawnedNote.Remove(prevnote);
            foreach (var item in spawnedNote)
            {
                if (item.line == prevnote.line)
                {
                    nextnote[prevnote.line] = item;
                    //Console.WriteLine("line" + prevnote.line + "updated");
                    break;
                }
            }
            if (prevnote == nextnote[prevnote.line])
            {
                nextnote[prevnote.line] = nullNote;
            }
        }
        public void UpdateLine(int i)
        //그 줄의 다음 노트를 가져옴
        //nextnote[i] = i줄의 다음 노트
        {
            bool set = false;
            foreach (var item in spawnedNote)
            {
                if (item.line == i && set == false)
                {
                    nextnote[i] = item;
                    //Console.WriteLine("line" + i + "updated");
                    set = true;
                    break;
                }
            }
            if (set == false)
            {
                nextnote[i] = nullNote;
            }
        }

        public void UpdateAcc()
        //acc 의 정의: 지금 키를 눌렀을때 ms와 노트의 ms의 차 (정확도)
        {
            for (int i = 0; i < 4; i++)
            {

                // 판정 범위 (단위 ms)
                const int RANKONEDECIDER = 10;
                const int PROPLUS = 18;
                const int PRO = 52;
                const int NOOB = 84;
                const int BAD = 160;
                const int MISS = 200;


                if (nextnote[i] != nullNote)
                //다음 노트가 존재한다면
                {
                    if (songms > 0)
                    {
                        acc[i] = nextnote[i].timing - songms;
                    }
                    else
                    {
                        acc[i] = 1000;
                    }
                    if (acc[i] < -BAD && !isLongNote[i])
                    {
                        if(nextnote[i].length > 0)
                        {
                            FailLongNote(nextnote[i]);
                        }
                        //Miss
                        UpdateLine(nextnote[i]);
                        //Console.WriteLine("reason 1");
                        Judgement(0);
                    }
                }
                else
                //다음 노트가 없다면 acc를 1000으로 고정
                {
                    acc[i] = 1000;
                }
                if (_keyChecker.keyDown(Setting.keySetting[i]))
                {
                    keyEffect[i] = 100;
                }
                if (_keyChecker.keyPress(Setting.keySetting[i]))
                {
                    if (acc[i] < PROPLUS && acc[i] > -PROPLUS)
                    {
                        
                        CheckLongNote(i);
                        Judgement(1);
                    }
                    else if (acc[i] < PRO && acc[i] > -PRO)
                    {
                       
                        CheckLongNote(i);
                        Judgement(2);
                    }
                    else if (acc[i] < NOOB && acc[i] > -NOOB)
                    {
                     
                        CheckLongNote(i);
                        Judgement(3);
                    }
                    else if (acc[i] < BAD && acc[i] > -BAD)
                    {
                
                        CheckLongNote(i);
                        Judgement(4);
                    }
                    else if (acc[i] < MISS)
                    {
                        if (nextnote[i].length > 0)
                        {
                            FailLongNote(nextnote[i]);
                        }
                        UpdateLine(nextnote[i]);
                        //Console.WriteLine("reason 2");
                        Judgement(0);
                    }
                }

            }
        }
        public void Judgement(int j)
        //판정 적용
        {
            int i = 0;
            comboEffect = 150;
            //콤보 애니메이션 재생진도
            maxscore += 100;
            //최대 점수 (정확도 판별에 쓰임)

            if (j != 0)
            {
                combo++;
            }
            if (j == 1)
            {
                i = 1;
                gameResult.score += 100;
                gameResult.subscore += 100;
                subMaxscore += 100;
                gameResult.proplus++;
                //Console.WriteLine("Pro+");
            }
            if (j == 2)
            {
                i = 2;
                gameResult.score += 100;
                subMaxscore += 100;
                gameResult.pro++;
                //Console.WriteLine("Pro");
            }
            if (j == 3)
            {
                i = 3;
                gameResult.score += 50;
                gameResult.noob++;
                //Console.WriteLine("Noob");
            }
            if (j == 4)
            {
                i = 4;
                gameResult.score += 30;
                gameResult.bad++;
                //Console.WriteLine("Bad");
            }
            if (j == 0)
            {
                gameResult.miss++;
                i = 0;
                combo = 0;
                //Console.WriteLine("Miss");
            }

            
            if (Setting.judgementDisplay[j])
            {
                judgementDisplay[0] = i;
                judgementDisplay[1] = 150;
            }
        }
    }
}

