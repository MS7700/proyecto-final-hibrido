import { useState } from "react";
import {
  List,
  Datagrid,
  TextField,
  BooleanField,
  Create,
  SimpleForm,
  NumberField,
  ReferenceField,
  NumberInput,
  ReferenceInput,
  SelectInput,
  DateField,
  DateInput,
  ShowButton,
  TextInput,
  Show,
  SimpleShowLayout,
  BooleanInput,
  DeleteButton,
  TabbedShowLayout,
  Tab,
  ReferenceManyField,
  ExportButton,
  downloadCSV,
  Button,
  useGetMany,
  useGetManyReference,
  useGetList,
  useDataProvider,
  useNotify,
} from "react-admin";
import { ShowSplitter } from "ra-compact-ui";
import Typography from "@material-ui/core/Typography";

import { unparse } from "papaparse";

const ExportNominaButton = (props) => {
  const dataProvider = useDataProvider();
  const [resumenes, setResumenes] = useState();
  const [detalles, setDetalles] = useState();
  const [loading, setLoading] = useState(false);
  const notify = useNotify();
  //const [csv, setCSV] = useState();

  const handleClick = () => {
    setLoading(true);
    //console.log(csv);
    if (!props.record) {
      setLoading(false);
      return;
    }
    const record = props.record;
    delete record["@odata.context"];
    let csv = unparse([record], { delimiter: ";" }) + "\n\n";
    
    
    //console.log(csv);
    setLoading(false);
    if(props.detallado){
      let resumen;
      let detalle;
      dataProvider.getManyReference('NominaResumen',
        {
          target: 'NominaID',
          id: record.id,
          pagination: { page: 1, perPage: 100000 },
          sort: { field: 'id', order: 'DESC' },
          filter: {}
        }
      )
      .then((data)=>resumen = data.data)
      .then(()=>resumen.forEach((r)=>{
        dataProvider.getManyReference(
          "NominaDetalle",
          {
            target: 'NominaResumenID',
            id: r.id,
            pagination: { page: 1, perPage: 100000 },
            sort: { field: 'id', order: 'DESC' },
            filter: {}
          }
        ).then((data)=>detalle = data.data)
        .then(()=> console.log(detalle))
        //.then(()=> console.log(unparse(detalle, { delimiter: ";" })))
        .then(()=>csv+=unparse([r], { delimiter: ";" }) + "\n")
        .then(()=> csv += unparse(detalle, { delimiter: ";" }) + "\n\n")
        .then(()=>console.log(csv))
      })
      )
      //.then(()=>console.log(resumen))
      //.then(()=>console.log(csv))
      .then((mycsv)=>downloadCSV(mycsv, "nomina"));
    }else{
      let resumen;
      dataProvider.getManyReference('NominaResumen',
        {
          target: 'NominaID',
          id: record.id,
          pagination: { page: 1, perPage: 100000 },
          sort: { field: 'id', order: 'DESC' },
          filter: {}
        }
      )
      .then((data)=>resumen = data.data)
      .then(()=>console.log(resumen))
      .then(()=>csv+=unparse(resumen, { delimiter: ";" }))
      .then(()=>downloadCSV(csv, "nomina"));
    } 

    
    /*
    console.log(props.detallado);
    if (props.detallado) {
      /*
      dataProvider.getManyReference('NominaResumen',
        {
          target: 'NominaID',
          id: record.id,
          pagination: { page: 1, perPage: 100000 },
          sort: { field: 'id', order: 'DESC' },
          filter: {}
        }
      )
        .then(r => setResumenes(r.data))
        .catch(e => notify(e))
        .then(
          resumenes.forEach(resumen => {
            setCSV(csv + unparse([resumen], { delimiter: ";" }) + "\n");
            dataProvider.getManyReference(
              "NominaDetalle",
              {
                target: 'NominaResumenID',
                id: resumen.id,
                pagination: { page: 1, perPage: 100000 },
                sort: { field: 'id', order: 'DESC' },
                filter: {}
              }
            ).then(d => setCSV(csv + unparse([d.data], { delimiter: ";" }) + "\n\n"));
          })
        ).then(console.log(csv));
      /*
      dataProvider.getManyReference('NominaResumen',
        {
          target: 'NominaID',
          id: record.id,
          pagination: { page: 1, perPage: 100000 },
          sort: { field: 'id', order: 'DESC' },
          filter: {}
        }
      )
        .then((data) => {
          //console.log(data);
          return Promise.all(data.data.map(
            (r) => {
              dataProvider.getManyReference(
                "NominaDetalle",
                {
                  target: 'NominaResumenID',
                  id: r.id,
                  pagination: { page: 1, perPage: 100000 },
                  sort: { field: 'id', order: 'DESC' },
                  filter: {}
                }
              )
                .then(
                  (data) => setDetalles(data.data)
                )//.then(console.log(detalles))
                .then(
                  dataProvider.getOne("Empleado", { id: r.EmpleadoID })
                    .then((e) => {
                      r.Empleado = e.data.Nombre;
                      delete r["EmpleadoID"];
                      delete r["NominaID"];
                      //setResumenes(r);
                    })
                  /*
                  .then(data => setResumenes(data))
                  .then(console.log(resumenes))
                  .then(() => {
                    //console.log(resumenes);
                    csv += unparse([resumenes], { delimiter: ";" })
                      + "\n"
                      + unparse(detalles, { delimiter: ";" }) + "\n\n";
                  })
                )
                /*
                .then(() => {
                  //console.log(resumenes);
                  csv += unparse(resumenes, { delimiter: ";" })
                    + "\n"
                    + unparse(detalles, { delimiter: ";" }) + "\n\n";
                })
                
                return r;
            }).then(
              data => setResumenes(data)
            ).then(console.log(resumenes))
              .then(() => {
                //console.log(resumenes);
                csv += unparse([resumenes], { delimiter: ";" })
                  + "\n"
                  + unparse(detalles, { delimiter: ";" }) + "\n\n";
              })
          )
        }
        )
        .then(() => {
          console.log(csv);
          //downloadCSV(csv, "nomina");
        }
        ).catch(e => setLoading(false));
      setLoading(false);
      */
     /*
    }
    else {
      dataProvider.getManyReference('NominaResumen',
        {
          target: 'NominaID',
          id: record.id,
          pagination: { page: 1, perPage: 100000 },
          sort: { field: 'id', order: 'DESC' },
          filter: {}
        }
      )
      .then((data) => (
          Promise.all(data.data.map(
            (r) => dataProvider.getOne("Empleado", { id: r.EmpleadoID }).then((e) => {
              r.Empleado = e.data.Nombre;
              delete r["EmpleadoID"];
              delete r["NominaID"];
              console.log(r);
              return r;
            })
          )).then(data => setResumenes(data))//.then(console.log(resumenes));
        )
        )
        .then(() => {
          console.log(resumenes);

          setCSV(csv + unparse(resumenes, { delimiter: ";" }));
          console.log(csv);
          //downloadCSV(csv, "nomina");
        }
        ).catch(e =>
          setLoading(false));
      setLoading(false);
    }
    */
  }
  
  return <Button {...props} onClick={handleClick} disabled={loading} />;
}


/*
const ExportNominaButton = (props) => {
  /*
  const dispatch = useDispatch();
  const redirect = useRedirect();
  const notify = useNotify();
  
  const record = props.record;
  console.log(record);
 
  const [loading, setLoading] = useState(false);
  const nominaResumenReference = useGetManyReference(
    "NominaResumen",
    "NominaID",
    record.id,
    {},
    {},
    {},
    "Nomina"
  );
  const nominadetallereference = useGetList("NominaDetalle");
 
  const nominaDetalleReferenceTable = [];
  const useGetNominaDetalleReference = n => [...Array(n)].map((_,i) => useGetManyReference(
    "NominaResumen",
    "NominaID",
    i,
    {},
    {},
    {},
    "Nomina"
  ));
 
 console.log(nominadetallereference.data)
  const nominaDetalleReferenceTable = [];
  const nominaDetalleTable = [];
  nominaResumenReference.ids.forEach((id) => {
    // eslint-disable-next-line react-hooks/rules-of-hooks
    
    nominaDetalleReferenceTable.push(
      nominadetallereference
    );
    nominaDetalleTable.push(
      nominadetallereference.data
    );
  });
  
  let nominaResumen = nominaResumenReference.data;
  const handleClick = () => {
    //console.log(nominaDetalleReferenceTable);
    //console.log(nominaDetalleTable);
    delete record["@odata.context"];
    let csv = unparse([record]) + "\n\n";
    if (props.detallado) {
        
        var i = 0;
        nominaResumenReference.ids.forEach((id) => {
          delete record[id]["NominaID"];
        const tableDetalle = [];
        csv += unparse([nominaResumen[id]]) + "\n";
        console.log([nominaResumen[id]]);
         
        nominaDetalleReferenceTable[i].ids.forEach(
          (ndID) => {
            //console.log(nominaDetalleTable[i][ndID]);
            tableDetalle.push(nominaDetalleTable[i][ndID]);
          }
        );
        csv += unparse(tableDetalle) + "\n\n";
       i++;
      });

    } 
    
    else {
      const tableResumen = [];
      nominaResumenReference.ids.forEach((id) => {
        delete record[id]["NominaID"];
        tableResumen.push(nominaResumen[id]);
      });
      csv += unparse(tableResumen) + "\n";
    }

    console.log(csv);
    downloadCSV(csv, "nomina");
  };
  
  return <Button {...props} onClick={handleClick} disabled={loading} />;
};
*/
const Validations = (values) => {
  const errors = {};
  if (!values.Fecha) {
    errors.Fecha = "La fecha es requerida.";
  }
  if (!values.TipoNominaID) {
    errors.TipoNominaID = "El tipo de nómina es requerido.";
  }
  return errors;
};

const Filters = [
  <NumberInput label="ID" source="id" />,
  <TextInput label="Nombre" source="Nombre" />,
  <DateInput source="Fecha" />,
  <TextInput label="Periodo" source="Periodo" />,
  <ReferenceInput
    label="Tipo de Nómina"
    source="TipoNominaID"
    reference="TipoNomina"
  >
    <SelectInput label="Tipo de Nómina" optionText="Descripcion" />
  </ReferenceInput>,
  <BooleanInput source="Contabilizado" />,
];

export const NominaList = (props) => (
  <List {...props} filters={Filters} sort={{ field: "Fecha", order: "DESC" }}>
    <Datagrid>
      <TextField source="id" />
      <DateField source="Fecha" />
      <TextField source="Periodo" />
      <ReferenceField
        label="Tipo de Nómina"
        source="TipoNominaID"
        reference="TipoNomina"
      >
        <TextField source="Descripcion" />
      </ReferenceField>
      <BooleanField source="Contabilizado" />
      <ShowButton />
      <DeleteButton />
    </Datagrid>
  </List>
);

const AsideInfo = (props) => (
  <SimpleShowLayout {...props}>
    <Typography variant="h6">Nómina</Typography>
    <DateField source="Fecha" />
    <TextField source="Periodo" />
    <ReferenceField
      label="Tipo de Nómina"
      source="TipoNominaID"
      reference="TipoNomina"
    >
      <TextField source="Descripcion" />
    </ReferenceField>
    <BooleanField source="Contabilizado" />
    <DeleteButton />
    <ExportNominaButton label="Exportar Detallado" detallado={true} />
    <ExportNominaButton label="Exportar Resumido" detallado={false} />
  </SimpleShowLayout>
);

const ResumenTab = () => (
  <ReferenceManyField reference="NominaResumen" target="NominaID">
    <Datagrid>
      <ReferenceField label="Nómina" source="NominaID" reference="Nomina">
        <TextField source="Periodo" />
      </ReferenceField>
      <ReferenceField label="Empleado" source="EmpleadoID" reference="Empleado">
        <TextField source="Nombre" />
      </ReferenceField>
      <NumberField source="SueldoBruto" />
      <NumberField source="SueldoDevengado" />
    </Datagrid>
  </ReferenceManyField>
);

const DetalleTab = () => (
  <ReferenceManyField
    label="Nómina Resumen"
    reference="NominaResumen"
    target="NominaID"
  >
    <Datagrid>
      <ReferenceField label="Empleado" source="EmpleadoID" reference="Empleado">
        <TextField source="Nombre" />
      </ReferenceField>
      <NumberField source="SueldoBruto" />
      <NumberField source="SueldoDevengado" />
      <ReferenceManyField
        label="Detalle"
        reference="NominaDetalle"
        target="NominaResumenID"
      >
        <Datagrid>
          <TextField source="Tipo" />
          <TextField source="Descripción" />
          <NumberField source="Monto" />
        </Datagrid>
      </ReferenceManyField>
    </Datagrid>
  </ReferenceManyField>
);

export const NominaShow = (props) => (
  <Show {...props} component="div" actions={<ExportButton />}>
    <ShowSplitter
      leftSideProps={{
        md: 2,
      }}
      rightSideProps={{
        md: 10,
      }}
      leftSide={<AsideInfo />}
      rightSide={
        <TabbedShowLayout>
          <Tab label="Resumido">
            <ResumenTab />
          </Tab>
          <Tab label="Detallado">
            <DetalleTab />
          </Tab>
        </TabbedShowLayout>
      }
    />
  </Show>
);

export const NominaCreate = (props) => (
  <Create {...props}>
    <SimpleForm validate={Validations}>
      <DateInput source="Fecha" />
      <ReferenceInput
        label="Tipo de Nómina"
        source="TipoNominaID"
        reference="TipoNomina"
      >
        <SelectInput label="Tipo de Nómina" optionText="Descripcion" />
      </ReferenceInput>
    </SimpleForm>
  </Create>
);
