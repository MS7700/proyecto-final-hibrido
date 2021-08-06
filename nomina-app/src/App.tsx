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
import { NominaList, NominaEdit, NominaCreate } from './entity/nomina';
import { EmpleadoList, EmpleadoEdit, EmpleadoCreate} from './entity/empleado';
import { UsuarioList, UsuarioEdit, UsuarioCreate} from './entity/usuario';
import authProvider from './authProvider';

function App() {
  const [dataProvider, setDataProvider] = useState<OdataDataProvider>();
  useEffect(() => {
    if(sessionStorage.getItem("token") !== "null"){
        odataProvider("https://localhost:44340/", () => {
          return Promise.resolve()
              .then((token) => ({
                  headers: {
                      Authorization: "Basic " + sessionStorage.getItem('token'),
                      "Access-Control-Allow-Headers": "Authorization","Access-Control-Allow-Origin": "*", 
                      "Access-Control-Allow-Methods": "GET, POST, DELETE'"
                  },
              }));
      }).then((provider) => setDataProvider(provider));
        return () => {};
    }else{
        odataProvider(
          "https://localhost:44340/"
        ).then((p) => setDataProvider(p));
        return () => {};
    }
  }, []);

  return dataProvider ? (
    //@ts-ignore
      <Admin title="Nómina APP" dataProvider={dataProvider} authProvider={authProvider}>
      {
        permissions => [
          <Resource name="Empleado" list={EmpleadoList} edit={EmpleadoEdit} create={EmpleadoCreate} />,
          <Resource name="Puesto" list={PuestoList} edit={PuestoEdit} create={PuestoCreate} />,
          <Resource name="Departamento" list={DepartamentoList} edit={DepartamentoEdit} create={DepartamentoCreate} />,
          <Resource name="Nomina" list={NominaList} edit={NominaEdit} create={NominaCreate} />,
          permissions.includes("admin") ? <Resource name="Usuario" list={UsuarioList} edit={UsuarioEdit} create={UsuarioCreate} /> : null
        ]
      }
    </Admin>
  ) : (
    <Loading loadingPrimary="Cargando..." loadingSecondary="Espere un momento"></Loading>
  );
}


export default App;
