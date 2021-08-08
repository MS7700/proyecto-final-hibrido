import React from 'react';
import logo from './logo.svg';
import './App.css';
import odataProvider, { OdataDataProvider } from "@ms7700/ra-data-odata-server-forked";
import { Admin, Resource, ListGuesser,EditGuesser,ShowGuesser,Loading } from 'react-admin';
import { useEffect, useState } from "react";
import { IngresoList, IngresoEdit, IngresoCreate } from './entity/ingreso';
import { DeduccionList, DeduccionEdit, DeduccionCreate } from './entity/deduccion';
import { DepartamentoList, DepartamentoEdit, DepartamentoCreate } from './entity/departamento';
import { PuestoList, PuestoEdit, PuestoCreate } from './entity/puesto';
import { TipoNominaList, TipoNominaEdit, TipoNominaCreate } from './entity/tiponomina';
import { NominaList, NominaEdit, NominaCreate } from './entity/nomina';
import { EmpleadoList, EmpleadoEdit, EmpleadoCreate} from './entity/empleado';
import { TransaccionList, TransaccionEdit, TransaccionCreate } from './entity/transaccion';
import { UsuarioCreate, UsuarioEdit, UsuarioList } from './entity/usuario';

function App() {
  const [dataProvider, setDataProvider] = useState<OdataDataProvider>();
  useEffect(() => {
    odataProvider(
      "https://localhost:44340/"
    ).then((p) => setDataProvider(p));
    return () => {};
  }, []);
  return dataProvider ? (
    <Admin dataProvider={dataProvider}>
      <Resource name="Empleado" list={EmpleadoList} edit={EmpleadoEdit} create={EmpleadoCreate} />
        <Resource name="Puesto" list={PuestoList} edit={PuestoEdit} create={PuestoCreate} />
        <Resource name="Departamento" list={DepartamentoList} edit={DepartamentoEdit} create={DepartamentoCreate} />
        <Resource name="Nomina" list={ListGuesser} edit={EditGuesser} create={NominaCreate} />
        <Resource name="TipoNomina" list={TipoNominaList} edit={TipoNominaEdit} create={TipoNominaCreate}/>
        <Resource name="NominaResumen" list={ListGuesser} edit={EditGuesser} />
        <Resource name="NominaDetalle" list={ListGuesser} edit={EditGuesser} />
        <Resource name="AsientoContable" list={ListGuesser} edit={EditGuesser} />
        <Resource name="Cuenta" list={UsuarioList} edit={UsuarioEdit} create={UsuarioCreate}/>
        <Resource name="TipoDeduccion" list={DeduccionList} edit={DeduccionEdit} create={DeduccionCreate} />
          <Resource name="TipoIngreso" list={IngresoList} edit={IngresoEdit} create={IngresoCreate} />
          <Resource name="Transaccion" list={TransaccionList} edit={TransaccionEdit} create={TransaccionCreate} />
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
