import "./MainPage.css"
import UserProfileList from "../Components/UserProfileList"
import HomeIcon from '@mui/icons-material/Home'
import React, { useEffect, useState } from "react";
import { Fragment } from "react/jsx-runtime"
import { AppBar, Avatar, Card, CardHeader, Grid2, IconButton, Stack,Container, TextField, Toolbar, Fab, Paper, Divider } from "@mui/material";

function MainPage(){
    return(
        <Grid2 container spacing={1} padding={2} paddingBottom={2} paddingRight={2} style={{height:"100dvh"}}>
            <Grid2 size={0.4} minWidth={"50px"}>
                {/*SideBar*/}

                <Avatar><HomeIcon/></Avatar>
            </Grid2>
            <Grid2 size={2} minWidth={"150px"}>
                {/*Second SideBar*/}
                <Card style={{width: "100%", height:"98dvh"}}>
                    <UserProfileList width="100%" height="100%"/>
                </Card>
            </Grid2>
            <Grid2 size={"grow"}>
                <Paper style={{borderRadius:"0px", padding:"10px"}} elevation={4}>
                    <Avatar></Avatar>
                </Paper>    
                <Card style={{height:"92dvh", borderRadius:"0px"}}>
                    <Grid2 container>
                        <Grid2 size={9}>
                            <Grid2 container direction={"column"}>
                                
                                <Grid2 height={"80dvh"}>
                                    
                                </Grid2>
                                <Grid2 alignContent={"center"} padding={2}>
                                    <TextField style={{width:"90%", marginRight:"20px"}} placeholder="Type messages."/>
                                    <Fab color="primary">

                                    </Fab>
                                </Grid2>
                            </Grid2>
                        </Grid2>
                        <Grid2 size={"grow"}>
                            
                        </Grid2>
                    </Grid2>
                </Card>
            </Grid2>
        </Grid2>
            
    );
}

export default MainPage;