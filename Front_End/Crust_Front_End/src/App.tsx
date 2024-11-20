import './App.css'
import { ApolloClient, InMemoryCache, ApolloProvider, HttpLink, from} from '@apollo/client'
import GetUser from "./Components/GetUser"
import MainPage from './Pages/MainPage';

const link = from([
  new HttpLink({uri: "http://156.67.216.145:90/graphql/"}),
]);

const client = new ApolloClient({
  cache: new InMemoryCache(),
  link: link,
});

function App() {
  return <ApolloProvider client={client}>
    <MainPage>
      
    </MainPage>
    
  </ApolloProvider>
}

export default App
