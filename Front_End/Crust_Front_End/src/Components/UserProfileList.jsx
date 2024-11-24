import { useEffect, useState } from "react";
import GetUser from "./Conn/GetUser";
import { Card, Stack, TextField, Paper, ListItem, Grid, List, ListItemText, Typography, Avatar, Grid2, Skeleton } from "@mui/material";

function UserProfileList(width, height){

    const [user, setUser] = useState([])

    const StoreRecieveUser = (userArray) => {
        setUser(userArray.user)
    }

    useEffect(() => {
        if( user === undefined){
            
        }else{
            console.log(user)
        }
    },[user])

    if(user === undefined && user.length === 0){return <div>Loading...</div>}

    return <div>
        <GetUser result={StoreRecieveUser}/>
        <div style={{heigh:{height}, minHeight:{height}, width:width, backgroundColor:"transparent"}}>
            <Stack style={{padding:"10px"}}>
                <TextField size="small" placeholder="Search..."/>
                <Typography variant="p" style={{marginTop:"10px", marginLeft:"5px", height:"10px",color:"grayText"}}>Direct Message</Typography>
            </Stack>
            <List>
                {
                    user.map((EachUser,index) => (
                        <ListItem key={index}>
                            <Grid2 container columnGap={1}>
                                <Grid2 size={5} alignContent={"center"}>
                                    <Avatar/>
                                </Grid2>
                                <Grid2 size={6}>
                                    <ListItemText primary={EachUser.name} secondary={"loading.."}>
                                        
                                    </ListItemText>
                                </Grid2>
                                
                            </Grid2>
                        </ListItem>
                    ))
                }
                
            </List>
        </div>
    </div>
}

export default UserProfileList;