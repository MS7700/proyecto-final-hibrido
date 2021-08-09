
import './App.css';

import odataProvider, { OdataDataProvider } from "@ms7700/ra-data-odata-server-forked";
import { Admin, Resource, ListGuesser,EditGuesser,ShowGuesser,Loading } from 'react-admin';
import { useEffect, useState } from "react";

import { IngresoList, IngresoEdit, IngresoCreate } from './entity/ingreso';
import { DeduccionList, DeduccionEdit, DeduccionCreate } from './entity/deduccion';
import { DepartamentoList, DepartamentoEdit, DepartamentoCreate } from './entity/departamento';
import { PuestoList, PuestoEdit, PuestoCreate } from './entity/puesto';
import { TipoNominaList, TipoNominaEdit, TipoNominaCreate } from './entity/tiponomina';
import { NominaList, NominaShow, NominaCreate } from './entity/nomina';
import { EmpleadoList, EmpleadoEdit, EmpleadoCreate} from './entity/empleado';
import { TransaccionList, TransaccionEdit, TransaccionCreate } from './entity/transaccion';
import { UsuarioCreate, UsuarioEdit, UsuarioList } from './entity/usuario';
import { AsientoContableCreate,AsientoContableShow,  AsientoContableList } from './entity/asientocontable';
import { CuentaCreate, CuentaEdit, CuentaList } from './entity/cuenta';
import { NominaDetalleList } from './entity/nominadetalle';
import { NominaResumenList } from './entity/nominaresumen';

import EmpleadosIcon from '@material-ui/icons/People';
import UsuariosIcon from '@material-ui/icons/AccountCircle';
import DepartamentoIcon from '@material-ui/icons/Business';
import WorkIcon from '@material-ui/icons/Work';
import DeduccionIcon from '@material-ui/icons/Remove';
import IngresoIcon from '@material-ui/icons/Add';
import TransaccionIcon from '@material-ui/icons/AttachMoney';
import NominaIcon from '@material-ui/icons/MenuBook';
import CuentaIcon from '@material-ui/icons/AccountBalance';
import AsientoContableIcon from '@material-ui/icons/AccountBalanceWallet';
import TipoNominaIcon from '@material-ui/icons/Event';

import theme from './theme';
import { i18nProvider } from './i18nProvider';

function App() {
  const [dataProvider, setDataProvider] = useState<OdataDataProvider>();
  useEffect(() => {
    odataProvider(
      "https://localhost:44340/"
    ).then((p) => setDataProvider(p));
    return () => {};
  }, []);
  return dataProvider ? (
    //@ts-ignore
    <Admin locale="es" i18nProvider={i18nProvider} theme={theme} title="Nómina APP" dataProvider={dataProvider}>
        <Resource name="Empleado" list={EmpleadoList} edit={EmpleadoEdit} create={EmpleadoCreate} icon={EmpleadosIcon}/>
        <Resource name="Puesto" list={PuestoList} edit={PuestoEdit} create={PuestoCreate} icon={WorkIcon} />
        <Resource name="Departamento" list={DepartamentoList} edit={DepartamentoEdit} create={DepartamentoCreate} icon={DepartamentoIcon} />
        <Resource name="Nomina" list={NominaList} show={NominaShow} create={NominaCreate} options={{label:'Nóminas'}} icon={NominaIcon}/>
        <Resource name="TipoNomina" list={TipoNominaList} edit={TipoNominaEdit} create={TipoNominaCreate} options={{label:'Tipos de Nóminas'}} icon={TipoNominaIcon}/>
        <Resource name="NominaResumen" list={NominaResumenList} options={{label:'Resumen de Nóminas'}}  icon={NominaIcon}/>
        <Resource name="NominaDetalle" list={NominaDetalleList} options={{label:'Detalles de Nóminas'}}  icon={NominaIcon}/>
        <Resource name="AsientoContable" list={AsientoContableList} show={AsientoContableShow} create={AsientoContableCreate} options={{label:'Asientos Contables'}} icon={AsientoContableIcon}  />
        <Resource name="Cuenta" list={CuentaList} edit={CuentaEdit} create={CuentaCreate} options={{label:'Catálogo de Cuentas'}} icon={CuentaIcon}/>
        <Resource name="TipoDeduccion" list={DeduccionList} edit={DeduccionEdit} create={DeduccionCreate} options={{label:'Deducciones'}} icon={DeduccionIcon}/>
        <Resource name="TipoIngreso" list={IngresoList} edit={IngresoEdit} create={IngresoCreate} options={{label:'Ingresos'}} icon={IngresoIcon}/>
        <Resource name="Transaccion" list={TransaccionList} edit={TransaccionEdit} create={TransaccionCreate} options={{label:'Transacciones'}} icon={TransaccionIcon}/>
        <Resource name="Usuario" list={UsuarioList} edit={UsuarioEdit} create={UsuarioCreate} icon={UsuariosIcon}/>
    </Admin>
  ) : (
    <Loading></Loading>
  );
}

/*
function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}
*/
export default App;
