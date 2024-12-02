import "./MainPage.css"
import UserProfileList from "../Components/UserProfileList"
import HomeIcon from '@mui/icons-material/Home'
import React, { useEffect, useState } from "react";
import { AppBar, Avatar, Card, CardHeader, Grid2, IconButton, Stack,Container, TextField, Toolbar, Fab, Paper, Divider, Typography, Skeleton } from "@mui/material";
import { useParams } from "react-router";
import Layout from "../Components/Layout";
import ChatPanel from '../Components/ChatPanel'
import useLoginAsDev from "../Components/Connection/Login";
interface user{
    name: string;

}

function MainPage(){
    const [user, SetUser] = useState<user>();

    const logAsDev = useLoginAsDev();

    useEffect(() =>
    {
        const log = async () => {
            await logAsDev;
        }
        
        if(logAsDev && logAsDev.length !== 0){
            SetUser(logAsDev.login.loginToken.user)
            console.log(user)
        }else{
            log();
        }
        
    }, [user,logAsDev])

    return(
        <>
            <Layout>
                <Layout.Corner>
                    <Avatar><HomeIcon/></Avatar>
                </Layout.Corner>
                <Layout.Sidebar>
                    <Grid2 container>
                        <Grid2 height={"90dvh"} width={"100%"}>
                            <UserProfileList/>
                        </Grid2>
                        <Grid2 height={"10dvh"} width={"100%"}>
                            <Card style={{padding:"10px", height:"100%"}} elevation={5}>
                                <Stack direction={"row"} gap={1}>
                                    <Avatar/>
                                    <Typography 
                                        component={"p"} 
                                        alignSelf={"center"} 
                                        textOverflow={"ellipsis"} 
                                        width={"fit-content"} 
                                        overflow={"hidden"} 
                                        maxWidth={"60%"}>
                                        {user?user.name:
                                            <Skeleton width={"70px"}/>
                                        }
                                    </Typography>
                                </Stack>
                            </Card>
                        </Grid2>
                    </Grid2>
                </Layout.Sidebar>
                <Layout.Main>
                    <ChatPanel/>
                </Layout.Main>
            </Layout>
        </>
    );
}

export default MainPage;