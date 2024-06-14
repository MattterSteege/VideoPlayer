using System.Xml;
using VideoPlayer.Models;

public class MpdParser
{
    public VideoMPD Parse(string mpdFilePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(mpdFilePath);

        VideoMPD mpd = new VideoMPD();

        mpd.MediaPresentationDuration = xmlDoc.DocumentElement?.GetAttribute("mediaPresentationDuration");
        mpd.MinBufferTime = xmlDoc.DocumentElement?.GetAttribute("minBufferTime");
        mpd.Profiles = xmlDoc.DocumentElement?.GetAttribute("profiles");
        mpd.Type = xmlDoc.DocumentElement?.GetAttribute("type");

        Period period = new Period();
        period.AdaptationSet = new List<AdaptationSet>();

        foreach (XmlNode adaptationSetNode in xmlDoc.DocumentElement?.ChildNodes!)
        {
            //if comment
            if (adaptationSetNode.NodeType == XmlNodeType.Comment)
                continue;

            //Element, Name="Period" -> children
            if (adaptationSetNode.Name == "Period")
            {
                foreach (XmlNode periodNode in adaptationSetNode.ChildNodes)
                {
                    //if comment
                    if (periodNode.NodeType == XmlNodeType.Comment)
                        continue;

                    //Element, Name="AdaptationSet" -> children
                    if (periodNode.Name == "AdaptationSet")
                    {
                        AdaptationSet adaptationSet = new AdaptationSet();
                        adaptationSet.MaxHeight = periodNode.Attributes["maxHeight"]?.Value;
                        adaptationSet.MaxWidth = periodNode.Attributes["maxWidth"]?.Value;
                        adaptationSet.MimeType = periodNode.Attributes["mimeType"]?.Value;
                        adaptationSet.SegmentAlignment = Convert.ToBoolean(periodNode.Attributes["segmentAlignment"]?.Value);
                        adaptationSet.StartWithSAP = Convert.ToInt32(periodNode.Attributes["startWithSAP"]?.Value);

                        //Element, Name="SegmentTemplate" -> children
                        foreach (XmlNode segmentTemplateNode in periodNode.ChildNodes)
                        {
                            if (segmentTemplateNode.Name == "SegmentTemplate")
                            {
                                SegmentTemplate segmentTemplate = new SegmentTemplate();
                                segmentTemplate.Initialization = segmentTemplateNode.Attributes["initialization"]?.Value;
                                segmentTemplate.Media = segmentTemplateNode.Attributes["media"]?.Value;
                                segmentTemplate.StartNumber = Convert.ToInt32(segmentTemplateNode.Attributes["startNumber"]?.Value);
                                segmentTemplate.Timescale = Convert.ToInt32(segmentTemplateNode.Attributes["timescale"]?.Value);

                                //Element, Name="SegmentTimeline" -> children
                                foreach (XmlNode segmentTimelineNode in segmentTemplateNode.ChildNodes)
                                {
                                    if (segmentTimelineNode.Name == "SegmentTimeline")
                                    {
                                        segmentTemplate.SegmentTimeline = new List<Segment>();
                                        foreach (XmlNode sNode in segmentTimelineNode.ChildNodes)
                                        {
                                            if (sNode.NodeType == XmlNodeType.Element)
                                            {
                                                Segment segment = new Segment();
                                                segment.Duration = Convert.ToInt32(sNode.Attributes["d"]?.Value);
                                                if (sNode.Attributes["r"] != null)
                                                    segment.RepeatAfter = Convert.ToInt32(sNode.Attributes["r"].Value);
                                                segmentTemplate.SegmentTimeline.Add(segment);
                                            }
                                        }
                                    }
                                }

                                adaptationSet.SegmentTemplate = segmentTemplate;
                            }
                        }

                        //Element, Name="Representation" -> children
                        foreach (XmlNode representationNode in periodNode.ChildNodes)
                        {
                            if (representationNode.Name == "Representation")
                            {
                                Representation representation = new Representation();
                                representation.AudioSamplingRate = Convert.ToInt32(representationNode.Attributes["audioSamplingRate"]?.Value);
                                representation.Bandwidth = Convert.ToInt32(representationNode.Attributes["bandwidth"]?.Value);
                                representation.Codecs = representationNode.Attributes["codecs"]?.Value;
                                representation.FrameRate = representationNode.Attributes["frameRate"]?.Value;
                                representation.Height = Convert.ToInt32(representationNode.Attributes["height"]?.Value);
                                representation.Id = representationNode.Attributes["id"]?.Value;
                                representation.ScanType = representationNode.Attributes["scanType"]?.Value;
                                representation.Width = Convert.ToInt32(representationNode.Attributes["width"]?.Value);

                                // AudioChannelConfiguration parsing goes here
                                XmlNode audioChannelConfigurationNode =
                                    representationNode.SelectSingleNode("AudioChannelConfiguration");
                                if (audioChannelConfigurationNode != null)
                                {
                                    AudioChannelConfiguration audioChannelConfiguration = new AudioChannelConfiguration();
                                    audioChannelConfiguration.SchemeIdUri = audioChannelConfigurationNode.Attributes["schemeIdUri"]?.Value;
                                    audioChannelConfiguration.Value = audioChannelConfigurationNode.Attributes["value"]?.Value;
                                    representation.AudioChannelConfiguration = audioChannelConfiguration;
                                }

                                adaptationSet.Representation = representation;
    
                            }
                        }
                        
                        period.AdaptationSet.Add(adaptationSet);
                    }
                }
            }
        }
        
        mpd.Period = period;
        return mpd;
    }
}