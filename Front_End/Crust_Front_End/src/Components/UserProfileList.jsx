import { useEffect, useState } from "react";
import GetUser from "./Conn/GetUser";
import { Card, Stack, TextField, Paper, ListItem, Grid, List, ListItemText } from "@mui/material";

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
                <TextField/>
            </Stack>
            <List>
                {
                    user.map((EachUser,index) => (
                        <ListItem key={index}>
                            <ListItemText primary={EachUser.name} secondary="loading.."/>
                        </ListItem>
                    ))
                }
            </List>
        </div>
    </div>
}

export default UserProfileList;