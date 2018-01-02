using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Java21.Model;

namespace Java21.Data
{
    public class JavaDAL
    {
        public string conn;
        public JavaDAL(string connect)
        {
            conn = connect;
        }

        #region 用户表数据操作
        public DataSet getAllUser()
        {
            string sql = "select * from Users";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet searchAllUser(string user)
        {
            string sql = "select * from Users where name like @name or nickname like @name";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@name", "%" + user + "%");
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public UserInfo Login(string user)
        {
            string sql = "select * from Users where name=@name or email=@name";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@name", user);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    UserInfo info = new UserInfo();
                    info.ID = Convert.ToInt32(read["id"]);
                    info.UserName = read["name"].ToString();
                    info.UserPass = read["psd"].ToString();
                    info.Role = Convert.ToInt32(read["roles"]);
                    info.Gender = Convert.ToInt32(read["gender"]);
                    info.NickName = read["nickname"].ToString();
                    info.Email = read["email"].ToString();
                    info.QQ = "" + read["qq"];
                    info.Mobile = "" + read["mobile"];
                    info.Birthday = Convert.ToDateTime(read["birthday"]);
                    info.SetAddress(""+read["address"]);
                    info.Valid = Convert.ToBoolean(read["valid"]);
                    info.Mark = "" + read["mark"];
                    info.HeadImg = (read["head"].Equals(DBNull.Value) ? "/images/head.jpg" : read["head"].ToString());
                    return info;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public UserInfo Login(int userid)
        {
            string sql = "select * from Users where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", userid);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    UserInfo info = new UserInfo();
                    info.ID = Convert.ToInt32(read["id"]);
                    info.UserName = read["name"].ToString();
                    info.UserPass = read["psd"].ToString();
                    info.Role = Convert.ToInt32(read["roles"]);
                    info.Gender = Convert.ToInt32(read["gender"]);
                    info.NickName = read["nickname"].ToString();
                    info.Email = read["email"].ToString();
                    info.QQ = "" + read["qq"];
                    info.Mobile = "" + read["mobile"];
                    info.Birthday = Convert.ToDateTime(read["birthday"]);
                    info.SetAddress("" + read["address"]);
                    info.Valid = Convert.ToBoolean(read["valid"]);
                    info.Mark = "" + read["mark"];
                    info.HeadImg = (read["head"].Equals(DBNull.Value) ? "/images/head.jpg" : read["head"].ToString());
                    return info;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int Register(UserInfo info)
        {
            string sql = "insert into Users (name,psd,roles,nickname,gender,email,birthday,valid,cdate,mark,head) values (@name,@psd,@roles,@nick,@gender,@email,@birth,@valid,@date,@mark,@head)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@name", info.UserName),
                                       new SqlParameter("@psd", info.UserPass),
                                       new SqlParameter("@roles", info.Role),
                                       new SqlParameter("@nick", info.NickName),
                                       new SqlParameter("@gender", info.Gender),
                                       new SqlParameter("@email", info.Email),
                                       new SqlParameter("@birth", info.Birthday),
                                       new SqlParameter("@valid", true),
                                       new SqlParameter("@date", DateTime.Now),
                                       new SqlParameter("@mark", info.Mark),
                                       new SqlParameter("@head", info.HeadImg)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public object checkUserforName(string name)
        {
            string sql = "select COUNT(1) from Users where name=@name";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@name", name);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public object checkUserforEmail(string email)
        {
            string sql = "select COUNT(1) from Users where email=@email";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@email", email);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int updateUser(UserInfo info)
        {
            string sql = "update Users set roles=@roles,nickname=@nickname,gender=@gender,mark=@mark,head=@head where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@id", info.ID),
                                       new SqlParameter("@roles",info.Role),
                                       new SqlParameter("@nickname",info.NickName),
                                       new SqlParameter("@gender",info.Gender),
                                       new SqlParameter("@mark",info.Mark),
                                       new SqlParameter("@head", info.HeadImg)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int updateFillUser(UserInfo info)
        {
            string sql = "update Users set name=@name,roles=@roles,nickname=@nickname,gender=@gender,email=@email,qq=@qq,mobile=@mobile,birthday=@birthday,address=@address,valid=@valid,mark=@mark,head=@head where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@id", info.ID),
                                       new SqlParameter("@name", info.UserName),
                                       new SqlParameter("@roles",info.Role),
                                       new SqlParameter("@nickname",info.NickName),
                                       new SqlParameter("@gender",info.Gender),
                                       new SqlParameter("@email", info.Email),
                                       new SqlParameter("@qq", info.QQ),
                                       new SqlParameter("@mobile", info.Mobile),
                                       new SqlParameter("@birthday", info.Birthday),
                                       new SqlParameter("@address", info.Addr),
                                       new SqlParameter("@valid", info.Valid),
                                       new SqlParameter("@mark",info.Mark),
                                       new SqlParameter("@head", SqlDbType.VarChar, 200)
                                   };
            if (info.HeadImg != null)
                paras[12].Value = info.HeadImg;
            else
                paras[12].Value = DBNull.Value;
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int changePassword(int id, string password)
        {
            string sql = "update Users set psd=@psd where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@id",id),
                                       new SqlParameter("@psd",password)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int deleteUser(int id)
        {
            string sql = "delete from Users where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 注册码数据表操作
        public DataSet getAllKeys()
        {
            string sql = "select * from Keys order by id desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet getKey(string key)
        {
            string sql = "select * from Keys where keys=@key";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@key", key);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet getKey()
        {
            string sql = "select keys,(maxc-curr)as timed from Keys where ver=@key and (maxc-curr) > 0 order by timed desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@key", 's');
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public object getKeyCount(string key)
        {
            string sql = "select Count(1) from Keys where keys=@key";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@key", key);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int addKeys(string key, int max, string ver, string mark)
        {
            string sql = "insert into Keys (keys,maxc,curr,ver,cdate,mark) values (@key,@max,@cur,@ver,@date,@mark)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@key", key),
                                       new SqlParameter("@max", max),
                                       new SqlParameter("@cur", SqlDbType.Int),
                                       new SqlParameter("@ver", SqlDbType.Char, 1),
                                       new SqlParameter("@date", DateTime.Now),
                                       new SqlParameter("@mark", mark)
                                   };
            paras[2].Value = 0;
            paras[3].Value = ver;
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int registKey(string key)
        {
            string sql = "update Keys set curr=curr+1 where keys=@key and maxc-curr > 0";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@key", key);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int deleteKey(int id)
        {
            string sql = "delete from Keys where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 视频数据表操作
        public DataSet getAllVideo()
        {
            string sql = "select * from Video";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Video> getAllVideos()
        {
            string sql = "select * from Video";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                List<Video> video = new List<Video>();
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Video v = new Video();
                    v.id = Convert.ToInt32(read["id"]);
                    v.name = "" + read["name"];
                    v.url = "" + read["url"];
                    v.url2 = "" + read["url2"];
                    video.Add(v);
                }
                return video;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public object getVideoUrl(string id)
        {
            string sql = "select url from Video where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public Video getVideo(int id)
        {
            string sql = "select * from Video where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    Video v = new Video();
                    v.id = Convert.ToInt32(read["id"]);
                    v.name = "" + read["name"];
                    v.url = "" + read["url"];
                    v.url2 = "" + read["url2"];
                    return v;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int addVideo(string name, string url, string url2)
        {
            string sql = "insert into Video (name,url,url2) values (@name,@url,@url2)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@name", name),
                                       new SqlParameter("@url", url),
                                       new SqlParameter("@url2", url2)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int updateVideo(int id, string name, string url, string url2)
        {
            string sql = "update Video set name=@name,url=@url,url2=@url2 where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@id", id),
                                       new SqlParameter("@name", name),
                                       new SqlParameter("@url", url),
                                       new SqlParameter("@url2", url2)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int delVideo(int id)
        {
            string sql = "delete from Video where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 语录表数据操作
        public DataSet getQuotations()
        {
            string sql = "select * from Quotations where DATEDIFF(dd,cdate,GetDate())=0 or tips=1";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet getAllQuotations()
        {
            string sql = "select * from Quotations";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet getQuotation(int id)
        {
            string sql = "select * from Quotations where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int addQuotations(string value, bool tips)
        {
            string sql = "insert into Quotations values (@sentence,@date,@tip)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@sentence", value),
                                       new SqlParameter("@date", DateTime.Now),
                                       new SqlParameter("@tip", tips)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int updateQuotations(int id, string value, bool tips, DateTime cdate)
        {
            string sql = "update Quotations set sentence=@sentence,cdate=@date,tips=@tip where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@sentence", value),
                                       new SqlParameter("@date", cdate),
                                       new SqlParameter("@tip", tips),
                                       new SqlParameter("@id", id)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int delQuotations(int id)
        {
            string sql = "delete from Quotations where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 知识点表数据操作
        //public DataSet getKnowledge()
        //{
        //    string sql = "select * from Knowledge where valid=1 order by comment desc,edate desc";
        //    SqlConnection con = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    try
        //    {
        //        con.Open();
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        sda.Fill(ds);
        //        return ds;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //public DataSet getKnowledge(int count, int pageindex)
        //{
        //    string sql = "select top(" + count + ")* from Knowledge where valid=1 and id not in(select top(" + count + "*(" + pageindex + "-1)) id from Knowledge where valid=1 order by comment desc,edate desc) order by comment desc,edate desc";
        //    SqlConnection con = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    try
        //    {
        //        con.Open();
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        sda.Fill(ds);
        //        return ds;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        public DataSet getKnowledge(int id)
        {
            string sql = "select * from Article where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        //public DataSet getRecycleKnowledge()
        //{
        //    string sql = "select * from Knowledge where valid=0 order by edate desc";
        //    SqlConnection con = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    try
        //    {
        //        con.Open();
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        sda.Fill(ds);
        //        return ds;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //public int updateKnowledge(int id, string title, string artical, string user)
        //{
        //    string sql = "update Knowledge set title=@title,artical=@artical,edate=@edate,euser=@euser where id=@id";
        //    SqlConnection con = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    SqlParameter[] paras = {
        //                               new SqlParameter("@title", title),
        //                               new SqlParameter("@artical", artical),
        //                               new SqlParameter("@edate", DateTime.Now),
        //                               new SqlParameter("@euser", user),
        //                               new SqlParameter("@id", id)
        //                           };
        //    cmd.Parameters.AddRange(paras);
        //    try
        //    {
        //        con.Open();
        //        return cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        Log.e(e.Message);
        //        return -1;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //public int updateKnowledge(int id, bool valid)
        //{
        //    string sql = "update Knowledge set valid=@valid where id=@id";
        //    SqlConnection con = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    SqlParameter[] paras = {
        //                               new SqlParameter("@valid", valid),
        //                               new SqlParameter("@id", id)
        //                           };
        //    cmd.Parameters.AddRange(paras);
        //    try
        //    {
        //        con.Open();
        //        return cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        Log.e(e.Message);
        //        return -1;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //public int addKnowledgeRead(int id)
        //{
        //    string sql = "update Knowledge set cread=cread+1 where id=@id";
        //    SqlConnection con = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    SqlParameter para = new SqlParameter("@id", id);
        //    cmd.Parameters.Add(para);
        //    try
        //    {
        //        con.Open();
        //        return cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        Log.e(e.Message);
        //        return -1;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //public int addKnowledgeComment(int id)
        //{
        //    string sql = "update Knowledge set comment=@comment where id=@id";
        //    SqlConnection con = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    SqlParameter[] paras = {
        //                               new SqlParameter("@id", id),
        //                               new SqlParameter("@comment", DateTime.Now)
        //                           };
        //    cmd.Parameters.AddRange(paras);
        //    try
        //    {
        //        con.Open();
        //        return cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        Log.e(e.Message);
        //        return -1;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //public int addKnowledge(string title, string artical, string user)
        //{
        //    string sql = "insert into Knowledge (title,artical,cdate,cuser,edate,comment,cread,valid) values (@title,@artical,@date,@cuser,@edate,@comment,@cread,@valid)";
        //    SqlConnection con = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    DateTime start = DateTime.Now;
        //    SqlParameter[] paras = {
        //                               new SqlParameter("@title", title),
        //                               new SqlParameter("@artical", artical),
        //                               new SqlParameter("@date", start),
        //                               new SqlParameter("@cuser", user),
        //                               new SqlParameter("@comment", start),
        //                               new SqlParameter("@edate", start),
        //                               new SqlParameter("@cread", SqlDbType.Int),
        //                               new SqlParameter("@valid", true)
        //                           };
        //    paras[1].SqlDbType = SqlDbType.Text;
        //    paras[5].Value = 0;
        //    cmd.Parameters.AddRange(paras);
        //    try
        //    {
        //        con.Open();
        //        return cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        Log.e(e.Message);
        //        return -1;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //public int deleteKnowledge(int id)
        //{
        //    string sql = "delete from Knowledge where id=@id";
        //    SqlConnection con = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    SqlParameter para = new SqlParameter("@id", id);
        //    cmd.Parameters.Add(para);
        //    try
        //    {
        //        con.Open();
        //        return cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        Log.e(e.Message);
        //        return -1;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //public int clearKnowledge()
        //{
        //    string sql = "delete from Knowledge where valid=@valid";
        //    SqlConnection con = new SqlConnection(conn);
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    SqlParameter para = new SqlParameter("@valid", false);
        //    cmd.Parameters.Add(para);
        //    try
        //    {
        //        con.Open();
        //        return cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        Log.e(e.Message);
        //        return -1;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        #endregion

        #region 文章表数据操作
        public DataSet getArticle()
        {
            string sql = "select * from Article where valid=1 order by comment desc,edate desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet getArticle(int count, int pageindex)
        {
            string sql = "select top(" + count + ")* from Article where valid=1 and id not in(select top(" + count + "*(" + pageindex + "-1)) id from Article where valid=1 order by comment desc,edate desc) order by comment desc,edate desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet getArticle(int category, int count, int pageindex)
        {
            string sql = "select top(" + count + ")* from Article where valid=1 and category=@cate and id not in(select top(" + count + "*(" + pageindex + "-1)) id from Article where valid=1 and category=@cate order by comment desc,edate desc) order by comment desc,edate desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@cate", category);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public Article getArticle(int id)
        {
            string sql = "select * from Article where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    Article art = new Article()
                    {
                        id = Convert.ToInt32(read["id"]),
                        category = Convert.ToInt32(read["category"]),
                        title = read["title"].ToString(),
                        artical = read["artical"].ToString(),
                        cdate = Convert.ToDateTime(read["cdate"]),
                        cuser = read["cuser"].ToString(),
                        edate = Convert.ToDateTime(read["edate"]),
                        euser = "" + read["euser"],
                        cread = Convert.ToInt32(read["cread"]),
                        comment = Convert.ToDateTime(read["comment"]),
                        valid = Convert.ToBoolean(read["valid"])
                    };
                    return art;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet getRecycleArticle()
        {
            string sql = "select * from Article where valid=0 order by edate desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int updateArticle(int id, int category, string title, string artical, string user)
        {
            string sql = "update Article set title=@title,category=@cate,artical=@artical,edate=@edate,euser=@euser where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@title", title),
                                       new SqlParameter("@cate", category),
                                       new SqlParameter("@artical", artical),
                                       new SqlParameter("@edate", DateTime.Now),
                                       new SqlParameter("@euser", user),
                                       new SqlParameter("@id", id)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int updateArticle(int id, bool valid)
        {
            string sql = "update Article set valid=@valid where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@valid", valid),
                                       new SqlParameter("@id", id)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int addArticleRead(int id)
        {
            string sql = "update Article set cread=cread+1 where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int addArticleComment(int id)
        {
            string sql = "update Article set comment=@comment where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@id", id),
                                       new SqlParameter("@comment", DateTime.Now)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int addArticle(string title, int category, string artical, string user)
        {
            string sql = "insert into Article (title,category,artical,cdate,cuser,edate,comment,cread,valid) values (@title,@cate,@artical,@date,@cuser,@edate,@comment,@cread,@valid)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            DateTime start = DateTime.Now;
            SqlParameter[] paras = {
                                       new SqlParameter("@title", title),
                                       new SqlParameter("@cate", category),
                                       new SqlParameter("@artical", artical),
                                       new SqlParameter("@date", start),
                                       new SqlParameter("@cuser", user),
                                       new SqlParameter("@comment", start),
                                       new SqlParameter("@edate", start),
                                       new SqlParameter("@cread", SqlDbType.Int),
                                       new SqlParameter("@valid", true)
                                   };
            paras[2].SqlDbType = SqlDbType.Text;
            paras[7].Value = 0;
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int deleteArticle(int id)
        {
            string sql = "delete from Article where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int clearArticle()
        {
            string sql = "delete from Article where valid=@valid";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@valid", false);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 评论表数据操作
        public int addComment(int aid, string uid, int star, string comment)
        {
            string sql = "insert into Comment (aid,uid,star,comment,cdate) values (@aid,@uid,@star,@comment,@cdate)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@aid", aid),
                                       new SqlParameter("@uid", uid),
                                       new SqlParameter("@star", star),
                                       new SqlParameter("@comment", comment),
                                       new SqlParameter("@cdate", DateTime.Now)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int addComment(int aid, string uid, int star, string comment, string device)
        {
            string sql = "insert into Comment values (@aid,@uid,@star,@comment,@cdate,@device)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@aid", aid),
                                       new SqlParameter("@uid", uid),
                                       new SqlParameter("@star", star),
                                       new SqlParameter("@comment", comment),
                                       new SqlParameter("@cdate", DateTime.Now),
                                       new SqlParameter("@device", device)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet getComment(int aid)
        {
            //string sql = "select top(10)* from Comment where aid=@aid order by cdate desc";
            string sql = "select top(10)c.*,u.head,u.nickname,u.name,u.valid from Comment c join Users u on c.uid=u.name where aid=@aid order by cdate desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@aid", aid);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 未使用次方法
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public List<Comment> getComments(int aid)
        {
            string sql = "select top(10)c.*,u.head from Comment c join Users u on c.uid=u.name where aid=@aid order by cdate desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@aid", aid);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                List<Comment> list = new List<Comment>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Comment com = new Comment()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        aid = Convert.ToInt32(reader["aid"]),
                        uid = reader["uid"].ToString(),
                        //comment = show_content("" + reader["comment"]),
                        cdate = Convert.ToDateTime(reader["cdate"]),
                        device = "" + reader["device"],
                        head = reader["head"].Equals(DBNull.Value) ? "/images/head.jpg" : reader["head"].ToString()
                    };
                    list.Add(com);
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int deleteComment(int id)
        {
            string sql = "delete from Comment where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 反馈表数据操作
        public DataSet getFeedback()
        {
            string sql = "select * from Feedback order by id desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int addFeedback(string feed, string device, string contact)
        {
            string sql = "insert into Feedback values (@suggest,@device,@contact,@cdate)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@suggest", feed),
                                       new SqlParameter("@device", device),
                                       new SqlParameter("@contact", contact),
                                       new SqlParameter("@cdate", DateTime.Now)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int delFeedback(int id)
        {
            string sql = "delete from Feedback where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 分类表数据操作
        public DataSet getCategory()
        {
            string sql = "select * from Category";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Category> getCategorys()
        {
            string sql = "select * from Category";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                List<Category> list = new List<Category>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category com = new Category()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        catename = reader["catename"].ToString(),
                        remark = "" + reader["remark"]
                    };
                    list.Add(com);
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public Category getCategory(int id)
        {
            string sql = "select * from Category where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    Category cate = new Category()
                    {
                        id = Convert.ToInt32(read["id"]),
                        catename = "" + read["catename"],
                        remark = "" + read["remark"]
                    };
                    return cate;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int addCategory(string name, string remark)
        {
            string sql = "insert into Category (catename,remark) values (@catename,@remark)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@catename", name),
                                       new SqlParameter("@remark", remark)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int updateCategory(Category cate)
        {
            string sql = "update Category set catename=@catename,remark=@remark where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@id", cate.id),
                                       new SqlParameter("@catename", cate.catename),
                                       new SqlParameter("@remark", cate.remark)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int delCategory(int id)
        {
            string sql = "delete from Category where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 版本下载表数据操作
        public DataSet getDownloadList()
        {
            string sql = "select * from Download order by cdate desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public Download getDownloadList(int id)
        {
            string sql = "select * from Download where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    Download ver = new Download()
                    {
                        id = Convert.ToInt32(read["id"]),
                        version = "" + read["ver"],
                        url = "" + read["url"],
                        cdate = Convert.ToDateTime(read["cdate"])
                    };
                    return ver;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int addDownload(string ver, string url, DateTime cdate)
        {
            string sql = "insert into Download (ver,url,cdate) values (@ver,@url,@cdate)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@ver", ver),
                                       new SqlParameter("@url", url),
                                       new SqlParameter("@cdate", cdate)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int updateDownload(int id, string ver, string url, DateTime cdate)
        {
            string sql = "update Download set ver=@ver,url=@url,cdate=@cdate where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@id", id),
                                       new SqlParameter("@ver", ver),
                                       new SqlParameter("@url", url),
                                       new SqlParameter("@cdate", cdate)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int delDownload(int id)
        {
            string sql = "delete from Download where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 资料下载表数据操作
        public DataSet getMaterialList()
        {
            string sql = "select * from Material order by cdate desc";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public Material getMaterialList(int id)
        {
            string sql = "select * from Material where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    Material mat = new Material()
                    {
                        id = Convert.ToInt32(read["id"]),
                        title = "" + read["title"],
                        url = "" + read["url"],
                        cdate = Convert.ToDateTime(read["cdate"]),
                        psd = "" + read["psd"],
                        remark = "" +read["remark"],
                        download = Convert.ToInt32(read["download"])
                    };
                    return mat;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int addMaterial(string title, string url, string psd, string remark, int donwload)
        {
            string sql = "insert into Material (title,url,psd,cdate,remark,download) values (@title,@url,@psd,@cdate,@remark,@download)";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@title", title),
                                       new SqlParameter("@url", url),
                                       new SqlParameter("@psd", psd),
                                       new SqlParameter("@cdate", DateTime.Now),
                                       new SqlParameter("@remark", remark),
                                       new SqlParameter("@download", donwload)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int updateMaterial(int id, string title, string url, string psd, string remark)
        {
            string sql = "update Material set title=@title,url=@url,psd=@psd,cdate=@cdate,remark=@remark where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] paras = {
                                       new SqlParameter("@id", id),
                                       new SqlParameter("@title", title),
                                       new SqlParameter("@url", url),
                                       new SqlParameter("@psd", psd),
                                       new SqlParameter("@cdate", DateTime.Now),
                                       new SqlParameter("@remark", remark)
                                   };
            cmd.Parameters.AddRange(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int updateMaterialDowload(int id)
        {
            string sql = "update Material set download=download+1 where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter paras = new SqlParameter("@id", id);
            cmd.Parameters.Add(paras);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public int delMaterial(int id)
        {
            string sql = "delete from Material where id=@id";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter para = new SqlParameter("@id", id);
            cmd.Parameters.Add(para);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.e(e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 其他
        /// <summary>
        /// 通用分页存储过程，有条件查询，有排序字段，按照排序字段的降序排列
        /// </summary>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="CurrentCount">当前记录数量（页码*每页记录数）</param>
        /// <param name="TableName">表名称</param>
        /// <param name="Where">查询条件，例："ID>1000 AND Name like '%LiLinFeng%'" 排序条件，直接在后面加，例：" ORDER BY ID DESC,NAME ASC"</param>
        /// <param name="TotalCount">记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetList(string Order, int PageSize, int CurrentCount, string TableName, string Where, out int TotalCount)
        {
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("prPager", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] parmList = {
                                            new SqlParameter("@PageSize",PageSize),
                                            new SqlParameter("@CurrentCount",CurrentCount),
                                            new SqlParameter("@TableName",TableName),
                                            new SqlParameter("@Where",Where),
                                            new SqlParameter("@Order",Order),
                                            new SqlParameter("@TotalCount",SqlDbType.Int,4)
                                      };
            parmList[5].Direction = ParameterDirection.Output;
            cmd.Parameters.AddRange(parmList);
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                TotalCount = Convert.ToInt32(parmList[5].Value);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        } 
        #endregion
    }
}
