import React from 'react';
import logo from './logo.svg';
import './App.css';
import odataProvider, { OdataDataProvider } from "ra-data-odata-server";
import { Admin, Resource, ListGuesser,EditGuesser,ShowGuesser,Loading } from 'react-admin';
import { useEffect, useState } from "react";
import { IngresoList, IngresoEdit, IngresoCreate } from './entity/ingreso';
import { DeduccionList, DeduccionEdit, DeduccionCreate } from './entity/deduccion';
import { DepartamentoList, DepartamentoEdit, DepartamentoCreate } from './entity/departamento';
import { PuestoList, PuestoEdit, PuestoCreate } from './entity/puesto';
import { NominaList, NominaEdit, NominaCreate } from './entity/nomina';
import { EmpleadoList, EmpleadoEdit, EmpleadoCreate} from './entity/empleado';

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
        <Resource name="Nomina" list={NominaList} edit={NominaEdit} create={NominaCreate} />
    </Admin>
  ) : (
    <Loading></Loading>
  );
}

export default App;
