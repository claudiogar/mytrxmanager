import { Header } from './components/Header';
import { LastTransactions } from './components/LastTransactions';

function App() {
  return (
    <div className="App container-fluid">
      <Header/>
      <div className="container-fluid">
        <LastTransactions/>
      </div>
    </div>
  );
}

export default App;
