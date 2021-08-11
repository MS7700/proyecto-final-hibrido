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
} from "react-admin";
import { ShowSplitter } from "ra-compact-ui";
import Typography from "@material-ui/core/Typography";

import { unparse } from "papaparse";

const ExportNominaButton = (props) => {
  /*
  const dispatch = useDispatch();
  const redirect = useRedirect();
  const notify = useNotify();
  */
  const record = props.record;
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
  /*
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
  */
  const nominaDetalleReferenceTable = [];
  const nominaDetalleTable = [];
  nominaResumenReference.ids.map((id) => {
    // eslint-disable-next-line react-hooks/rules-of-hooks
    const nominadetallereference = useGetManyReference(
      "NominaDetalle",
      "NominaResumenID",
      id,
      {},
      {},
      {},
      "NominaResumen"
    );
    nominaDetalleReferenceTable.push(
      nominadetallereference
    );
    nominaDetalleTable.push(
      nominadetallereference.data
    );
    return;
  });
  
  let nominaResumen = nominaResumenReference.data;
  const handleClick = () => {
    //console.log(nominaDetalleReferenceTable);
    //console.log(nominaDetalleTable);
    delete record["@odata.context"];
    let csv = unparse([record]) + "\n\n";
    if (props.detallado) {
        
        var i = 0;
        nominaResumenReference.ids.map((id) => {
          delete record[id]["NominaID"];
        const tableDetalle = [];
        csv += unparse([nominaResumen[id]]) + "\n";
        console.log([nominaResumen[id]]);
         
        nominaDetalleReferenceTable[i].ids.map(
          (ndID) => {
            //console.log(nominaDetalleTable[i][ndID]);
            tableDetalle.push(nominaDetalleTable[i][ndID]);
            return;
          }
        );
        csv += unparse(tableDetalle) + "\n\n";
       i++;
        return;
      });

    } 
    
    else {
      const tableResumen = [];
      nominaResumenReference.ids.map((id) => {
        delete record[id]["NominaID"];
        return tableResumen.push(nominaResumen[id]);
      });
      csv += unparse(tableResumen) + "\n";
    }

    console.log(csv);
    downloadCSV(csv, "nomina");
  };

  return <Button {...props} onClick={handleClick} disabled={loading} />;
};

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
