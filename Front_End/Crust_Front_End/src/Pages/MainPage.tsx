import "./MainPage.css"
import UserProfileList from "../Components/UserProfileList"
import React, { useEffect, useState } from "react";
import { Fragment } from "react/jsx-runtime"
import { AppBar, Avatar, Card, CardHeader, Grid2, IconButton, Stack, TextField, Toolbar, Fab, Paper } from "@mui/material";

function MainPage(){
    
    return(
        <Grid2 container spacing={1} padding={2} paddingBottom={0} paddingRight={0} style={{height:"100dvh"}}>
            <Grid2 size={0.4}>
                <Stack>
                    <Avatar/>
                </Stack>
                
            </Grid2>
            <Grid2 size={2}>
                <Card style={{width: "100%", height:"98dvh"}}>
                    <UserProfileList width="100%" height="100%"/>
                </Card>
            </Grid2>
            <Grid2 size={"grow"}>
                <Paper style={{borderRadius:"0px", padding:"10px"}}>
                    <Avatar></Avatar>
                </Paper>
                <Card style={{height:"92dvh", borderRadius:"0px"}} elevation={2}>
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
                            <Paper elevation={1} style={{height:"92dvh", width:"100%", borderRadius:"0px"}}>
                                
                            </Paper>
                        </Grid2>
                    </Grid2>
                </Card>
            </Grid2>
        </Grid2>
            
    );
}

export default MainPage;