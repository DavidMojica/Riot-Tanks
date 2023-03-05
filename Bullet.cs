using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotTanks_1._1
{
    public class Bullet : Graphic_Object
    {
        public int velocidad_bullet=18;
        string direccion;
        public string Direccion { get => direccion; set => direccion = value; }
        public Bullet() { } 

        public Bullet(int x, int y,string direccion) : base("bullet"+"_"+direccion, x+10, y+10, 8, 8)
        {
            this.Direccion = direccion;
        }


        public void mover_bullet(string direccion,Bullet bullet)
        {
            switch (this.Direccion)
            {
                case "up":
                    this.PosY -= velocidad_bullet;
                    SetPos(PosX, PosY);
                    break;
                case "down":
                    this.PosY += velocidad_bullet;
                    SetPos(PosX, PosY);
                    break;
                case "left":
                    this.PosX -= velocidad_bullet;
                    SetPos(PosX, PosY);
                    break;
                case "right":
                    this.PosX += velocidad_bullet;
                    SetPos(PosX, PosY);
                    break;
            }
        }
        public int colision_bullet(List<Bullet> bullets,List<Graphic_Object> envs)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < envs.Count; j++)
                {
                    
                    if (bullets[i].GetBounds().IntersectsWith(envs[j].GetBounds()))
                    {
                        if (envs[j].Colision_bala)
                        {
                            return i; //devolvemos id proyectil
                        }
                     }             
                }               
            }              
            return -1;
        }
        public int colision_bullet2(List<Bullet> bullets, List<Graphic_Object> envs)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < envs.Count; j++)
                {

                    if (bullets[i].GetBounds().IntersectsWith(envs[j].GetBounds()))
                    {
                        if (envs[j].impacto())
                        {
                            return j; //devolvemos id objeto del ambiente
                        }
                    }                               
                }
            }
            return -1;
        }
        public int colision_bullet3(List<Bullet> bullets, List<Tank> bots)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < bots.Count; j++)
                {

                    if (bullets[i].GetBounds().IntersectsWith(bots[j].GetBounds()))
                    {

                            return j; //Devolvemos el id del bot impactado
                        
                    }
                }
            }
            return -1;
        }
        public int colision_bullet4(List<Bullet> bullets, List<Tank> bots)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < bots.Count; j++)
                {

                    if (bullets[i].GetBounds().IntersectsWith(bots[j].GetBounds()))
                    {

                        return i; //Devolvemos el id del bullet que impactó al bot

                    }
                }
            }
            return -1;
        }
    }
}
