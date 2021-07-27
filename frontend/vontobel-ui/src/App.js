import { Header } from './components/Header';
import { LastTransactions } from './components/LastTransactions';
import { TransactionAdder } from './components/TransactionAdder';

function App() {
  return (
    <div className="App container-fluid">
      <Header/>
      <div className="container-fluid">
        <div className='row'>
          <div className='col-8'>
            <LastTransactions/>
          </div>
          <div className='col-4'>
            <TransactionAdder/>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
