import './App.css'
import { ApolloClient, InMemoryCache, ApolloProvider, HttpLink, from} from '@apollo/client'
import MainPage from './Pages/MainPage';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { CssBaseline } from '@mui/material';

const link = from([
  new HttpLink({uri: "http://156.67.216.145:90/graphql/"}),
]);

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
    primary:{
      main: '#31363F',
      light: '#76ABAE',
      dark: '#222831'
    },
    
  },
});

const client = new ApolloClient({
  cache: new InMemoryCache(),
  link: link,
});

function App() {
  return (
    <ThemeProvider theme={darkTheme} >
      <CssBaseline/>
      <ApolloProvider client={client}>
        <MainPage/>

      </ApolloProvider>
    </ThemeProvider>
  )
}

export default App
