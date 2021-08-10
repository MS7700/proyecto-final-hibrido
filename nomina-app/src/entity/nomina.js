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
} from "react-admin";
import { ShowSplitter } from "ra-compact-ui";
import Typography from "@material-ui/core/Typography";

import { unparse } from "papaparse";


  const ExportNominaButton = ({ record }) => {
  /*
  const dispatch = useDispatch();
  const redirect = useRedirect();
  const notify = useNotify();
  */
  const [loading, setLoading] = useState(false);
  const handleClick = () => {
    var table=[];
    delete record['@odata.context'];
    table.push(record);
    console.log(record);
    console.log(table);
    console.log([
      {
        "Column 1": "foo",
        "Column 2": "bar"
      }
    ]);
    const csv = unparse([
      record
    ]) + '\n' + unparse([
      record
    ]);
    console.log(csv);
    downloadCSV(csv,'nomina');
    /*
    setLoading(true);
    dispatch(fetchStart()); // start the global loading indicator
    const Record = { ...record };
    fetch(
      `https://localhost:44340/AsientoContable(${record.id})/Contabilidad.EnviarAsiento`,
      { method: "POST", body: Record }
    )
      .then(() => {
        notify("Asiento enviado a contabilidad");
        redirect("/AsientoContable");
      })
      .catch((e) => {
        notify(
          "Error: El asiento no pudo ser enviado correctamente",
          "warning"
        );
      })
      .finally(() => {
        setLoading(false);
        dispatch(fetchEnd()); // stop the global loading indicator
      });
      */
  };
  
  return (
    <Button
      label="Exportar"
      onClick={handleClick}
      disabled={loading}
    />
  );
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
    <ExportNominaButton />
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
