import React from 'react';
import { Avatar, Card, Grid2, Stack, TextField, Paper, Typography } from "@mui/material";

interface Layout{
}

function Layout({children}){

    const getChildrenOnDisplay = (children, Name)=>
        React.Children.map(children, (child)=>
            child.type.displayName == Name ? child: null
        );

    const corner = getChildrenOnDisplay(children, "Corner");
    const sidebar = getChildrenOnDisplay(children, "Sidebar");
    const main = getChildrenOnDisplay(children, "Main");

    return <>
        <Grid2 container spacing={1} padding={2} paddingBottom={2} paddingRight={2} style={{height:"98dvh"}}>
            <Grid2 size={0.4} minWidth={"50px"}>
                {/*SideBar*/}
                {corner}
            </Grid2>
            <Grid2 size={2} minWidth={"200px"} maxWidth={"250px"}>
                {/*Second SideBar*/}
                <Card style={{width: "100%", height:"97dvh"}}>
                    {sidebar}
                </Card>
            </Grid2>
            <Grid2 size={"grow"}>
                <Paper style={{borderRadius:"0px", padding:"10px"}} elevation={4}>
                    <Avatar></Avatar>
                </Paper>    
                <Card style={{height:"91dvh", borderRadius:"0px", minWidth:"500px"}} >
                    <Grid2 container >
                        <Grid2 size={9}>
                            <Grid2 padding={1}>
                                {main}
                                
                            </Grid2>
                        </Grid2>
                        <Grid2 size={"grow"}>
                            
                        </Grid2>
                    </Grid2>
                </Card>
            </Grid2>
        </Grid2>
    </>
}

const Sidebar = (props) => <div className='Layout-Sidebar'>{props.children}</div>;
const Corner = (props) => <div className='Layout-Corner'>{props.children}</div>;
const Main = (props) => <div className='Layout-Main'>{props.children}</div>;

Layout.Sidebar = Sidebar;
Layout.Corner = Corner;
Layout.Main = Main;

Main.displayName = "Main";
Sidebar.displayName = "Sidebar";
Corner.displayName = "Corner";



export default Layout;