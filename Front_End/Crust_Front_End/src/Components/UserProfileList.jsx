import { useEffect, useState } from "react";
import GetUser from "./Conn/GetUser";
import './UserProfileList.jsx.css'
import ImageIcon from '@mui/icons-material/Image';
import { TransitionGroup } from 'react-transition-group';
import { Card, Stack, TextField, Paper, ListItem, Grid, List, ListItemText, Typography, Avatar, Grid2, Skeleton, ListItemIcon, Collapse, Badge, styled } from "@mui/material";

function UserProfileList(width, height, Filter){

    const [user, setUser] = useState([])
    const [dUser, setdUser] = useState([])
    const [FilterText, SetFilter] = useState("")
    

    const StoreRecieveUser = (userArray) => {
        setUser(userArray.user)
    }

    useEffect(() => {
        if( user === undefined){
            
        }else{
            setdUser(user.filter(user => user.name.toLowerCase().includes(FilterText.toLowerCase())))
        }
    },[user, FilterText])

    if(user === undefined && user.length === 0){return <div>Loading...</div>}

    return <div>
        <GetUser result={StoreRecieveUser}/>
        <div style={{heigh:{height}, minHeight:{height}, width:width, backgroundColor:"transparent"}}>
            <Stack style={{padding:"10px"}} gap={"10px"}>
                <TextField size="small" placeholder="Search..." onChange={(e) => SetFilter(e.target.value)} value={FilterText}/>
                <Typography variant={"caption"} color={"textSecondary"} marginLeft={"10px"}>
                    Direct Message
                </Typography>
            </Stack>
            <List>
                <TransitionGroup>
                {
                    dUser.map((EachUser,index) => (
                        <Collapse key={index}>
                            <ListItem  style={{borderRadius:"10px"}} className="User">
                                <ListItemIcon>
                                <Badge badgeContent={6} invisible={false} slotProps={{badge:{className: "badge", style:{backgroundColor:"rgb(236, 100, 75)"}}}}><Avatar><ImageIcon/></Avatar></Badge>
                                </ListItemIcon>
                                <ListItemText primary={EachUser.name} secondary={"loading.."}></ListItemText>
                            </ListItem>
                        </Collapse>
                        
                    ))
                }
                </TransitionGroup>
            </List>
        </div>
    </div>


}



export default UserProfileList;

