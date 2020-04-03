import React, { SyntheticEvent } from "react";
import { Grid } from "semantic-ui-react";
import { IActivity } from "../../../app/models/activity";
import { ActivityList } from "./ActivityList";
import { ActivityDetails } from "../details/ActivityDetails";
import { ActivityForm } from "../form/ActivityForm";

interface IProps {
  activities: IActivity[];
  selectActivity: (id: string) => void;
  selectedActivity: IActivity;
  editMode: boolean;
  setEditMode: (editMode: boolean) => void;
  setSelectedActivity: (activity: IActivity | null) => void;
  createActivity: (activity: IActivity) => void;
  editActivity: (activity: IActivity) => void;
  deleteActivity:  (event: SyntheticEvent<HTMLButtonElement>, id: string) => void;
  submitting: boolean;
  target: string;
}

export const ActivityDashboard: React.FC<IProps> = props => {
  return (
    <Grid>
      <Grid.Column width={10}>
        <ActivityList
          activities={props.activities}
          selectActivity={props.selectActivity}
          deleteActivity={props.deleteActivity}
          submitting={props.submitting}
          target={props.target}
        />
      </Grid.Column>
      <Grid.Column width={6}>
        {props.selectedActivity && !props.editMode && (
          <ActivityDetails
            activity={props.selectedActivity}
            setEditMode={props.setEditMode}
            setSelectedActivity={props.setSelectedActivity}
          />
        )}
        {props.editMode && (
          <ActivityForm
            key={props.selectedActivity ? props.selectedActivity.id : 0}
            setEditMode={props.setEditMode}
            activity={props.selectedActivity}
            createActivity={props.createActivity}
            editActivity={props.editActivity}
            submitting={props.submitting}
          />
        )}
      </Grid.Column>
    </Grid>
  );
};
